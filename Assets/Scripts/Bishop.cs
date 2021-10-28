using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    protected override List<List<XZCoordinate>> GetPossibleMoves()
    {
        boardPosition.GetNumericCoordinates(out var x, out var z);

        var possibleMoves = DiagonalMovement(false);
        foreach (var coordinateList in possibleMoves)
        {
            coordinateList.ForEach(coordinate => Debug.Log(coordinate.X + " , " + coordinate.Z));
            coordinateList.RemoveAll(coordinate =>
                coordinate.X < 0 || coordinate.Z < 0 || coordinate.X > 7 || coordinate.Z > 7);
        }
        // return ChessBoard.AvailableMoves(possibleMoves, this);
        return possibleMoves;
        ;
    }
}