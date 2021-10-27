using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rook : ChessPiece
{
    protected override List<List<XZCoordinate>> GetPossibleMoves()
    {
        boardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = XAxisMovement(false).ToList();
        ZAxisMovement(false).ForEach(coordinateList => possibleMoves.Add(coordinateList));

        foreach (var cPossibleMove in possibleMoves)
        {
            cPossibleMove.ForEach(move => Debug.Log(move.X + " , " + move.Z));
        }
        return possibleMoves;
        // return ChessBoard.AvailableMoves(possibleMoves, this);
    }
}