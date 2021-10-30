using System.Collections.Generic;
using System.Linq;
using structs;

public class Queen : ChessPiece
{
    protected override List<List<XZCoordinate>> UnitSpecificMovement()
    {
        var moves = XAxisMovement(false).ToList();
        ZAxisMovement(false).ForEach(coordinateList => moves.Add(coordinateList));
        DiagonalMovement(false).ForEach(coordinateList => moves.Add(coordinateList));
        return moves;
    }
}