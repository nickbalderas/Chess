using System.Linq;

public class Queen : ChessPiece
{
    protected override void GetPossibleMoves()
    {
        BoardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = XAxisMovement(false).ToList();
        ZAxisMovement(false).ForEach(coordinateList => possibleMoves.Add(coordinateList));
        DiagonalMovement(false).ForEach(coordinateList => possibleMoves.Add(coordinateList));
        foreach (var move in ChessBoard.AvailableMoves(possibleMoves, this))
        {
            AvailableMoves.Add(move);
        }
    }
}