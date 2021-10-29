using System.Collections.Generic;

public class Pawn : ChessPiece
{
    protected override void GetPossibleMoves()
    {
        BoardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = ZAxisMovement(true, _hasMoved ? 1 : 2);
        foreach (var move in ChessBoard.AvailableMoves(possibleMoves, this))
        {
            AvailableMoves.Add(move);
        }
    }

    public List<XZCoordinate> GetDiagonalMoves()
    {
        var list = new List<XZCoordinate>();
        foreach (var coordinateList in DiagonalMovement(true, 1))
        {
            foreach (var move in coordinateList)
            {
                list.Add(move);
            }
        }
        return list;
    }
}