using System.Collections.Generic;

public class Knight : ChessPiece
{
    protected override List<List<XZCoordinate>> GetPossibleMoves()
    {
        boardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = new List<List<XZCoordinate>>
        {
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x - 1, z + 2)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x - 1, z - 2)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x + 1, z + 2)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x + 1, z - 2)}),

            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x + 1, z - 2)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x - 1, z - 2)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x + 1, z + 2)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x - 1, z + 2)}),
        };
        return possibleMoves;
        // return ChessBoard.AvailableMoves(possibleMoves, this);
    }
}
