using System;
using UnityEngine;

public class BoardSquare
{
    private readonly GridXZ<BoardSquare> _grid;
    public ChessPiece chessPiece;
    public string x;
    public string z;

    public BoardSquare(GridXZ<BoardSquare> grid, string x, string z)
    {
        _grid = grid;
        this.x = x;
        this.z = z;
    }

    public void SetChessPiece(ChessPiece chessPiece)
    {
        this.chessPiece = chessPiece;
        _grid.TriggerGridObjectChanged(x, z);
    }

    public void RemoveChessPiece()
    {
        chessPiece = null;
        _grid.TriggerGridObjectChanged(x, z);
    }

    public bool CanBuild()
    {
        return chessPiece == null;
    }

    public void GetNumericCoordinates(out int x, out int z)
    {
        x = Array.IndexOf(ChessBoard.XAxisValues, this.x);
        z = Array.IndexOf(ChessBoard.ZAxisValues, this.z);
    }

    public bool IsSelf(BoardSquare boardSquare)
    {
        return x == boardSquare.x && z == boardSquare.z;
    }

    public override string ToString()
    {
        return x + z + "\n" + chessPiece;
    }
}
