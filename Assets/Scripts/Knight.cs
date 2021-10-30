using System.Collections.Generic;
using structs;

public class Knight : ChessPiece
{
    protected override void GetMoves()
    {
        BoardPosition.GetNumericCoordinates(out var x, out var z);
        var possibleMoves = new List<List<XZCoordinate>>
        {
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x - 1, z + 2)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x - 1, z - 2)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x + 1, z + 2)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x + 1, z - 2)}),

            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x + 2, z - 1)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x + 2, z + 1)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x - 2, z - 1)}),
            new List<XZCoordinate>(new List<XZCoordinate> {new XZCoordinate(x - 2, z + 1)}),
        };
        
        foreach (var set in possibleMoves)
        {
            set.RemoveAll(coordinate =>
                coordinate.X < 0 || coordinate.Z < 0 || coordinate.X > 7 || coordinate.Z > 7);
        }
        
        foreach (var move in ChessBoard.AvailableMoves(possibleMoves, this))
        {
            AvailableMoves.Add(move);
        }
    }
}
