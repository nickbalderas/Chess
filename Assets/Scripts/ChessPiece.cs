using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
   public BoardSquare boardPosition;
   public bool isLight;
   public bool isKnight;
   public bool isPawn;
   
   private void Update()
   {
       if (!Input.GetMouseButtonDown(0)) return;
        
       var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       if (!Physics.Raycast(ray, out var raycastHit, float.MaxValue)) return;
       ChessBoard.Grid.GetXZ(raycastHit.point, out var x, out var z);
       BoardSquare boardSquare = ChessBoard.Grid.GetGridObject(x, z);

       if (!boardPosition.IsSelf(boardSquare)) return;
       GetPossibleMoves();
   }
   
   protected List<List<XZCoordinate>> ZAxisMovement([NotNull] bool isRestricted, [CanBeNull] int limit = 0)
   {
       if (isKnight) return null;
       boardPosition.GetNumericCoordinates(out var x, out var z);
       var forwardSet = new List<XZCoordinate>();
       var backwardSet = new List<XZCoordinate>();

       // Decrease toward front
       for (int i = z - 1; isRestricted ? i >= z - limit : i >= 0; i--)
       {
           if (isLight && isPawn) break;
           if (i < 0) break;
           backwardSet.Add(new XZCoordinate(x, i));
       }

       // Increase toward back
       for (int i = z + 1; isRestricted ? i <= z + limit : i <= 7; i++)
       {
           if (!isLight && isPawn) break;
           if (i > 7) break;
           forwardSet.Add(new XZCoordinate(x, i));
       }
       
       var zAxisMovement = new List<List<XZCoordinate>> {backwardSet, forwardSet};
       foreach (var set in zAxisMovement)
       {
           set.RemoveAll(coordinate =>
               coordinate.X < 0 || coordinate.Z < 0 || coordinate.X > 7 || coordinate.Z > 7);
       }
       return zAxisMovement;
   }

   protected List<List<XZCoordinate>> XAxisMovement([NotNull] bool isRestricted, [CanBeNull] int limit = 0)
   {
       if (isKnight) return null;
       boardPosition.GetNumericCoordinates(out var x, out var z);
       var leftSet = new List<XZCoordinate>();
       var rightSet = new List<XZCoordinate>();

       // Decrease toward left
       for (int i = x - 1; isRestricted ? i >= x - limit : i >= 0; i--)
       {
           if (isLight && isPawn) break;
           if (i < 0) break;
           leftSet.Add(new XZCoordinate(i, z));
       }

       // Increase toward right
       for (int i = x + 1; isRestricted ? i <= x + limit : i <= 7; i++)
       {
           if (!isLight && isPawn) break;
           if (i > 7) break;
           rightSet.Add(new XZCoordinate(i, z));
       }
       
       var xAxisMovement = new List<List<XZCoordinate>> {leftSet, rightSet};
       foreach (var set in xAxisMovement)
       {
           set.RemoveAll(coordinate =>
               coordinate.X < 0 || coordinate.Z < 0 || coordinate.X > 7 || coordinate.Z > 7);
       }
       return xAxisMovement;
   }

   protected List<List<XZCoordinate>> DiagonalMovement(bool isRestricted, int value = 0)
   {
       if (isKnight) return null;
       boardPosition.GetNumericCoordinates(out var x, out var z);
       var backRightSet = new List<XZCoordinate>();
       var backLeftSet = new List<XZCoordinate>();
       var frontLeftSet = new List<XZCoordinate>();
       var frontRightSet = new List<XZCoordinate>();

       if (isPawn && isLight)
       {
           backRightSet = BackRightGridMovement(x, z, isRestricted, value);
           backLeftSet = BackLeftGridMovement(x, z, isRestricted, value);
       } else if (isPawn && !isLight)
       {
           frontLeftSet = FrontLeftGridMovement(x, z, isRestricted, value);
           frontRightSet = FrontRightGridMovement(x, z, isRestricted, value);
       }
       else
       {
           backRightSet = BackRightGridMovement(x, z);
           backLeftSet = BackLeftGridMovement(x, z);
           frontLeftSet = FrontLeftGridMovement(x, z);
           frontRightSet = FrontRightGridMovement(x, z);
       }

       var diagonalMovement = new List<List<XZCoordinate>> {backLeftSet, backRightSet, frontLeftSet, frontRightSet};
       foreach (var set in diagonalMovement)
       {
           set.RemoveAll(coordinate =>
               coordinate.X < 0 || coordinate.Z < 0 || coordinate.X > 7 || coordinate.Z > 7);
       }
       return diagonalMovement;
   }

   protected virtual List<XZCoordinate> GetPossibleMoves()
   {
       return null;
   }
   
   private List<XZCoordinate> BackRightGridMovement(int x, int z, bool isRestricted = false, int value = 0)
   {
       var coordinates = new List<XZCoordinate>();
       for (int i = x + 1, j = z + 1; GetCondition(x, z, i, j); i++, j++)
       {
           coordinates.Add(new XZCoordinate(i, j));
       }

       bool GetCondition(int x, int z, int i, int j)
       {
           return isRestricted ? i == x + value && j == z + value : i <= 7 && j <= 7;
       }

       return coordinates;
   }
   
   private List<XZCoordinate> BackLeftGridMovement(int x, int z, bool isRestricted = false, int value = 0)
   {
       var coordinates = new List<XZCoordinate>();
       for (int i = x - 1, j = z + 1; GetCondition(x, z, i, j); i--, j++)
       {
           coordinates.Add(new XZCoordinate(i, j));
       }
       
       bool GetCondition(int x, int z, int i, int j)
       {
           return isRestricted ? i == x - value && j == z + value : i >= 0 && j <= 7;
       }

       return coordinates;
   }
   
   private List<XZCoordinate> FrontLeftGridMovement(int x, int z, bool isRestricted = false, int value = 0)
   {
       var coordinates = new List<XZCoordinate>();
       for (int i = x - 1, j = z - 1; GetCondition(x, z, i, j); i--, j--)
       {
           coordinates.Add(new XZCoordinate(i, j));
       }
       
       bool GetCondition(int x, int z, int i, int j)
       {
           return isRestricted ? i == x - value && j == z - value : i >= 0 && j >= 0;
       }

       return coordinates;
   }
   
   private List<XZCoordinate> FrontRightGridMovement(int x, int z, bool isRestricted = false, int value = 0)
   {
       var coordinates = new List<XZCoordinate>();
       for (int i = x + 1, j = z - 1; GetCondition(x, z, i, j); i++, j--)
       {
           coordinates.Add(new XZCoordinate(i, j));
       }
       
       bool GetCondition(int x, int z, int i, int j)
       {
           return isRestricted ? i == x + value && j == z - value : i <= 7 && j >= 0;
       }

       return coordinates;
   }

}
