using System.Collections.Generic;
using System.Linq;
using structs;

public class King : ChessPiece
{
    protected override List<List<XZCoordinate>> UnitSpecificMovement()
    {
        var moves = XAxisMovement(true, 1).ToList();
        ZAxisMovement(true, 1).ForEach(coordinateList => moves.Add(coordinateList));
        DiagonalMovement(true, 1).ForEach(coordinateList => moves.Add(coordinateList));
        return moves;
    }
}