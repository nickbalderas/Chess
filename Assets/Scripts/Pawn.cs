using System.Collections.Generic;
using structs;

public class Pawn : ChessPiece
{
    protected override List<List<XZCoordinate>> UnitSpecificMovement()
    {
        return ZAxisMovement(true, HasMoved ? 1 : 2);
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