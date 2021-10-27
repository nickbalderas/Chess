namespace structs
{
    public struct BoardSquare
    {
        public XZCoordinate Coordinate;
        public ChessPiece ChessPiece;

        public BoardSquare(XZCoordinate coordinate, ChessPiece chessPiece)
        {
            Coordinate = coordinate;
            ChessPiece = chessPiece;
        }
    }
}