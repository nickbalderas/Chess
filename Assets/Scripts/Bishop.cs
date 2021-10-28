using System.Collections.Generic;

public class Bishop : ChessPiece
{
    protected override List<XZCoordinate> GetPossibleMoves()
    {
        boardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = DiagonalMovement(false);
        return ChessBoard.AvailableMoves(possibleMoves, this);
    }
}