using System;
using System.Collections.Generic;
using structs;
using UnityEngine;

public class Pawn : ChessPiece
{
    private enum PawnPromotionPiece
    {
        Rook,
        Knight,
        Bishop,
        Queen,
    }
    
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

        if (isLight && z == 7 || !isLight && z == 0)
        {
            GameManager.uiController.ShowPawnPromotionUI();
            GameManager.uiController.pawnToRookButton.onClick.AddListener(() => HandlePawnPromotion(grid, squareToMoveTo, PawnPromotionPiece.Rook));
            GameManager.uiController.pawnToKnightButton.onClick.AddListener(() => HandlePawnPromotion(grid, squareToMoveTo, PawnPromotionPiece.Knight));
            GameManager.uiController.pawnToBishopButton.onClick.AddListener(() => HandlePawnPromotion(grid, squareToMoveTo, PawnPromotionPiece.Bishop));
            GameManager.uiController.pawnToQueenButton.onClick.AddListener(() => HandlePawnPromotion(grid, squareToMoveTo, PawnPromotionPiece.Queen));
        }
        else base.HandleMovement(grid, squareToMoveTo);
    }

    private void HandlePawnPromotion(GridXZ<BoardSquare> grid, BoardSquare squareToMoveTo, PawnPromotionPiece promotionPiece)
    {
        squareToMoveTo.GetNumericCoordinates(out var x, out var z);
        
        var selectedOption = promotionPiece + (AssignedPlayer.IsAssignedLight ? "Light" : "Dark");
        var type = (ChessPieceFactory.ChessPieceID) Enum.Parse(typeof(ChessPieceFactory.ChessPieceID), selectedOption);
        var selectedPiece = ChessPieceFactory.GetChessPiece(type);
        
        var pawnPromotedUnit = Instantiate(selectedPiece, grid.GetWorldPosition(x,z), Quaternion.identity);
        pawnPromotedUnit.BoardPosition = squareToMoveTo;
        pawnPromotedUnit.AssignedPlayer = AssignedPlayer;
        squareToMoveTo.SetChessPiece(pawnPromotedUnit);
        
        GameManager.uiController.HidePawnPromotionUI();
        Destroy(gameObject);
    }
}