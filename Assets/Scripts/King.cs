using System.Collections.Generic;
using System.Linq;

public class King : ChessPiece
{
    protected override void GetPossibleMoves()
    {
        BoardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = XAxisMovement(true, 1).ToList();
        ZAxisMovement(true, 1).ForEach(coordinateList => possibleMoves.Add(coordinateList));
        foreach (var move in ChessBoard.AvailableMoves(possibleMoves, this))
        {
            AvailableMoves.Add(move);
        }
    }
}