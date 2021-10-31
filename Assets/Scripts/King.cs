using System;
using System.Collections.Generic;
using System.Linq;
using structs;
using UnityEngine;

public class King : ChessPiece
{
    protected override List<List<XZCoordinate>> UnitSpecificMovement()
    {
        var moves = XAxisMovement(true, CanCastleQueenSide() ? 2 : 1, CanCastleKingSide() ? 2 : 1).ToList();
        ZAxisMovement(true, 1).ForEach(coordinateList => moves.Add(coordinateList));
        DiagonalMovement(true, 1).ForEach(coordinateList => moves.Add(coordinateList));
        return moves;
    }

    public override void HandleMovement(GridXZ<BoardSquare> grid, BoardSquare squareToMoveTo)
    {
        if (AvailableMoves.Count <= 0 || !AvailableMoves.Contains(squareToMoveTo)) return;
        
        squareToMoveTo.GetNumericCoordinates(out var x, out _);
        BoardPosition.GetNumericCoordinates(out var h, out _);
        var kingDidCastle = classification == PieceType.King && Math.Abs(h - x) == 2;

        if (kingDidCastle)
        {
            var currentRookXPos = x > h ? 7 : 0;
            var desiredRookXPos = x > h ? 5 : 3;
            
            var rookToMove = ChessBoard.Grid.GetGridObject(currentRookXPos, isLight ? 0 : 7);
            var newRookPosition = ChessBoard.Grid.GetGridObject(desiredRookXPos, isLight ? 0 : 7);
            Destroy(rookToMove.ChessPiece.gameObject);
            var movedRook = Instantiate(rookToMove.ChessPiece, grid.GetWorldPosition(desiredRookXPos, isLight ? 0 : 7), Quaternion.identity);
            movedRook.hasMoved = true;
            newRookPosition.SetChessPiece(movedRook);
            newRookPosition.ChessPiece.BoardPosition = newRookPosition;
            newRookPosition.ChessPiece.AssignedPlayer = AssignedPlayer;
        }
        
        base.HandleMovement(grid, squareToMoveTo);
    }

    private bool CanCastleKingSide()
    {
        var rookZPos = isLight ? 0 : 7;
        var kingSideRook = ChessBoard.Grid.GetGridObject(7, rookZPos)?.ChessPiece;
        if (!kingSideRook) return false;
        
        var isPathObstructed = false;
        for (var i = 5; i < 7; i++)
        {
            if (!ChessBoard.Grid.GetGridObject(i, rookZPos).ChessPiece) continue;
            isPathObstructed = true;
            break;
        }

        return !hasMoved && !kingSideRook.hasMoved && !isPathObstructed;
    }

    private bool CanCastleQueenSide()
    {
        var rookZPos = isLight ? 0 : 7;
        var queenSideRook = ChessBoard.Grid.GetGridObject(0, rookZPos)?.ChessPiece;
        if (!queenSideRook) return false;
        
        var isPathObstructed = false;
        for (var i = 1; i < 4; i++)
        {
            if (!ChessBoard.Grid.GetGridObject(i, rookZPos).ChessPiece) continue;
            isPathObstructed = true;
            break;
        }

        return !hasMoved && !queenSideRook.hasMoved && !isPathObstructed;
    }
}