using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    private bool _hasMoved;
    
    protected override List<List<XZCoordinate>> GetPossibleMoves()
    {
        boardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = ZAxisMovement(true, _hasMoved ? 1 : 2);
        DiagonalMovement(true, 1).ForEach(coordinateList => possibleMoves.Add(coordinateList));
        foreach (var cPossibleMove in possibleMoves)
        {
            cPossibleMove.ForEach(move => Debug.Log(move.X + " , " + move.Z));
        }
        return possibleMoves;
        // return ChessBoard.AvailableMoves(possibleMoves, this);
    }
}