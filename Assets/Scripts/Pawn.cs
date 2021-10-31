using System.Collections.Generic;
using structs;

public class Pawn : ChessPiece
{
    protected override List<List<XZCoordinate>> UnitSpecificMovement()
    {
        return ZAxisMovement(true, hasMoved ? 1 : 2);
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
    
    public override void HandleMovement(GridXZ<BoardSquare> grid, BoardSquare squareToMoveTo)
    {
        if (AvailableMoves.Count <= 0 || !AvailableMoves.Contains(squareToMoveTo)) return;
        squareToMoveTo.GetNumericCoordinates(out var x, out var z);
       // if (isLight && z == 7 || !isLight && z == 0) HandlePawnPromotion(grid, squareToMoveTo);
        base.HandleMovement(grid, squareToMoveTo);
    }

    // public void HandlePawnPromotion(GridXZ<BoardSquare> grid, BoardSquare boardSquare)
    // {
    //     Destroy(gameObject);
    //     boardSquare.GetNumericCoordinates(out var x, out var z);
    //     var pawnPos = ChessBoard.Grid.GetGridObject(x, z);
    //     var possiblePieces = AssignedPlayer.OpposingPlayer.EliminatedPieces;
    //     
    //     
    //     var optionData = new List<string>();
    //     optionData.Add("");
    //     foreach (var piece in possiblePieces)
    //     {
    //         optionData.Add(piece.name);
    //         
    //     }


        // if (possiblePieces.Count <= 0)
        // {
        //     base.HandleMovement(grid, boardSquare);
        //     return;
        // }
        // var test = possiblePieces[0];
        //
        // test.gameObject.transform.position = ChessBoard.Grid.GetWorldPosition(x, z);
        // pawnPos.SetChessPiece(test);
        // pawnPos.ChessPiece.BoardPosition = pawnPos;
        // pawnPos.ChessPiece.AssignedPlayer = AssignedPlayer;
        // pawnPos.ChessPiece.gameObject.SetActive(true);

        // var pawnPromotedUnit = Instantiate(AssignedPlayer.OpposingPlayer.EliminatedPieces[0], ChessBoard.Grid.GetWorldPosition(x,z), Quaternion.identity);
        // pawnPos.SetChessPiece(pawnPromotedUnit);
        // pawnPos.ChessPiece.BoardPosition = pawnPos;
        // pawnPos.ChessPiece.AssignedPlayer = AssignedPlayer;
    // }
}