using System.Collections.Generic;
using System.Linq;

public class Rook : ChessPiece
{
    protected override List<XZCoordinate> GetPossibleMoves()
    {
        boardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = XAxisMovement(false).ToList();
        ZAxisMovement(false).ForEach(coordinateList => possibleMoves.Add(coordinateList));
        return ChessBoard.AvailableMoves(possibleMoves, this);
    }
}