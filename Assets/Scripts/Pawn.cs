using System.Collections.Generic;

public class Pawn : ChessPiece
{
    private bool _hasMoved;
    
    protected override List<XZCoordinate> GetPossibleMoves()
    {
        boardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = ZAxisMovement(true, _hasMoved ? 1 : 2);
        DiagonalMovement(true, 1).ForEach(coordinateList => possibleMoves.Add(coordinateList));
        return ChessBoard.AvailableMoves(possibleMoves, this);
    }
}