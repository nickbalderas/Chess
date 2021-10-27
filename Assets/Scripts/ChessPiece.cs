using System;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    // Grid Position - BoardSquare
    // HowToMove

    public BoardSquare boardPosition;

    private void Awake()
    {
        
    }

    public void GetNumericCoordinates(BoardSquare boardSquare, out int x, out int z)
    {
        x = Array.IndexOf(ChessBoard.XAxisValues, boardSquare.X);
        z = Array.IndexOf(ChessBoard.ZAxisValues, boardSquare.Z);
    }

    public virtual List<XZCoordinate> GetPossibleMoves()
    {
        return null;
    }
}
