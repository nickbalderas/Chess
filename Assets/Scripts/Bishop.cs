using System.Collections.Generic;
using structs;

public class Bishop : ChessPiece
{
    protected override List<List<XZCoordinate>> UnitSpecificMovement()
    {
        return DiagonalMovement(false);
    }
}