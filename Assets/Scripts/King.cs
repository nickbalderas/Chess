using System.Collections.Generic;
using System.Linq;

public class King : ChessPiece
{
    protected override List<XZCoordinate> GetPossibleMoves()
    {
        boardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = XAxisMovement(true, 1).ToList();
        ZAxisMovement(true, 1).ForEach(coordinateList => possibleMoves.Add(coordinateList));
        return ChessBoard.AvailableMoves(possibleMoves, this);
    }
}