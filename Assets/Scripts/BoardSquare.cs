using System;

public class BoardSquare
{
    private readonly GridXZ<BoardSquare> _grid;
    public BoardSquareVisual BoardSquareVisual;
    public ChessPiece ChessPiece;
    private readonly string _x;
    private readonly string _z;

    public BoardSquare(GridXZ<BoardSquare> grid, string x, string z, BoardSquareVisual boardSquareVisual)
    {
        _grid = grid;
        _x = x;
        _z = z;
        BoardSquareVisual = boardSquareVisual;
    }

    public void SetBoardSquareVisual(BoardSquareVisual boardSquareVisual)
    {
        BoardSquareVisual = boardSquareVisual;
        _grid.TriggerGridObjectChanged(_x, _z);
    }

    public void SetChessPiece(ChessPiece chessPiece)
    {
        ChessPiece = chessPiece;
        _grid.TriggerGridObjectChanged(_x, _z);
    }

    public void GetNumericCoordinates(out int x, out int z)
    {
        x = Array.IndexOf(ChessBoard.XAxisValues, this._x);
        z = Array.IndexOf(ChessBoard.ZAxisValues, this._z);
    }

    public bool IsSelf(BoardSquare boardSquare)
    {
        return _x == boardSquare._x && _z == boardSquare._z;
    }

    public override string ToString()
    {
        return _x + _z + "\n" + ChessPiece;
    }
}
