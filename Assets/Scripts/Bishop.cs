public class Bishop : ChessPiece
{
    protected override void GetPossibleMoves()
    {
        BoardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = DiagonalMovement(false);
        foreach (var move in ChessBoard.AvailableMoves(possibleMoves, this))
        {
            AvailableMoves.Add(move);
        }
    }
}