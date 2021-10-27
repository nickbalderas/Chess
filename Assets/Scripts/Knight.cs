using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Knight : ChessPiece
{
    public override List<XZCoordinate> GetPossibleMoves()
    {
        GetNumericCoordinates(base.boardPosition, out var x, out var z);
        Debug.Log("Numeric Coordinates: " + x + " , " + z);
        var possibleMoves = new List<XZCoordinate>
        {
            new XZCoordinate(x - 1, z + 2),
            new XZCoordinate(x - 1, z - 2),
            new XZCoordinate(x + 1, z + 2),
            new XZCoordinate(x + 1, z - 2)
        };

        var knightMovesInverted = new List<XZCoordinate>();

        foreach (var move in possibleMoves)
        {
            knightMovesInverted.Add(new XZCoordinate(move.Z, move.X));
        }

        possibleMoves = possibleMoves.Concat(knightMovesInverted).ToList();
        
        return possibleMoves;
    }
}
