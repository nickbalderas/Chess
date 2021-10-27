public class BoardSquare
{
    private GridXZ<BoardSquare> _grid;
    public string X;
    public string Z;
    public ChessPiece ChessPiece;

    public BoardSquare(GridXZ<BoardSquare> grid, string x, string z)
    {
        _grid = grid;
        X = x;
        Z = z;
    }

    public void SetChessPiece(ChessPiece chessPiece)
    {
        ChessPiece = chessPiece;
        _grid.TriggerGridObjectChanged(X, Z);
    }

    public void ClearTransform()
    {
        this.ChessPiece = null;
        _grid.TriggerGridObjectChanged(X, Z);
    }

    public bool CanBuild()
    {
        return ChessPiece == null;
    }

    public override string ToString()
    {
        return X + Z + "\n" + ChessPiece;
    }
}
