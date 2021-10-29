using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public BoardSquare BoardPosition;
    public readonly List<BoardSquare> AvailableMoves = new List<BoardSquare>();
    private Outline _highlight;
    public bool isLight;
    public bool isKnight;
    public bool isPawn;
    protected bool _hasMoved;

    private void Awake()
    {
        _highlight = GetComponentInChildren<Outline>();
        _highlight.enabled = false;
    }

    public void Selected()
    {
        GetPossibleMoves();
        HighlightPossibleMoves(true);
        HighlightChessPiece(true);
    }

    public void Unselected()
    {
        HighlightPossibleMoves(false);
        HighlightChessPiece(false);
        AvailableMoves.Clear();
    }

    protected virtual void GetPossibleMoves()
    {
    }

    private void HighlightPossibleMoves(bool indicator)
    {
        foreach (var move in AvailableMoves)
        {
            move.BoardSquareVisual.Highlight(indicator, move.ChessPiece && move.ChessPiece.isLight != isLight);
        }
    }

    private void HighlightChessPiece(bool indicator)
    {
        if (indicator && _highlight.enabled) return;
        if (!indicator && _highlight.enabled) _highlight.enabled = false;
        if (indicator && !_highlight.enabled)
        {
            _highlight.enabled = true;
            _highlight.OutlineMode = Outline.Mode.OutlineAll;
            _highlight.OutlineColor = Color.blue;
            _highlight.OutlineWidth = 10f;
        }
    }

    public void HandleChessPieceMovement(GridXZ<BoardSquare> grid, BoardSquare squareToMoveTo)
    {
        if (AvailableMoves.Count <= 0 || !AvailableMoves.Contains(squareToMoveTo)) return;
        
        Unselected();
        Destroy(gameObject);
        squareToMoveTo.GetNumericCoordinates(out var x, out var z);
        var movedChessPiece = Instantiate(this, grid.GetWorldPosition(x, z), Quaternion.identity);
        movedChessPiece._hasMoved = true;
        squareToMoveTo.SetChessPiece(movedChessPiece);
        squareToMoveTo.ChessPiece.BoardPosition = squareToMoveTo;
    }

    protected List<List<XZCoordinate>> ZAxisMovement(bool isRestricted, int limit = 0)
    {
        if (isKnight) return null;
        BoardPosition.GetNumericCoordinates(out var x, out var z);
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

    protected IEnumerable<List<XZCoordinate>> XAxisMovement(bool isRestricted, int limit = 0)
    {
        if (isKnight) return null;
        BoardPosition.GetNumericCoordinates(out var x, out var z);
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
        BoardPosition.GetNumericCoordinates(out var x, out var z);
        var backRightSet = new List<XZCoordinate>();
        var backLeftSet = new List<XZCoordinate>();
        var frontLeftSet = new List<XZCoordinate>();
        var frontRightSet = new List<XZCoordinate>();

        if (isPawn && isLight)
        {
            backRightSet = BackRightGridMovement(x, z, isRestricted, value);
            backLeftSet = BackLeftGridMovement(x, z, isRestricted, value);
        }
        else if (isPawn && !isLight)
        {
            frontLeftSet = FrontLeftGridMovement(x, z, isRestricted, value);
            frontRightSet = FrontRightGridMovement(x, z, isRestricted, value);
        }
        else
        {
            backRightSet = BackRightGridMovement(x, z, isRestricted, value);
            backLeftSet = BackLeftGridMovement(x, z, isRestricted, value);
            frontLeftSet = FrontLeftGridMovement(x, z, isRestricted, value);
            frontRightSet = FrontRightGridMovement(x, z, isRestricted, value);
        }

        var diagonalMovement = new List<List<XZCoordinate>> {backLeftSet, backRightSet, frontLeftSet, frontRightSet};
        foreach (var set in diagonalMovement)
        {
            set.RemoveAll(coordinate =>
                coordinate.X < 0 || coordinate.Z < 0 || coordinate.X > 7 || coordinate.Z > 7);
        }

        return diagonalMovement;
    }

    private static List<XZCoordinate> BackRightGridMovement(int x, int z, bool isRestricted = false, int value = 0)
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

    private static List<XZCoordinate> BackLeftGridMovement(int x, int z, bool isRestricted = false, int value = 0)
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

    private static List<XZCoordinate> FrontLeftGridMovement(int x, int z, bool isRestricted = false, int value = 0)
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

    private static List<XZCoordinate> FrontRightGridMovement(int x, int z, bool isRestricted = false, int value = 0)
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