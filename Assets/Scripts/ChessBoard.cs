using System.Collections.Generic;
using UnityEngine;
using BoardSquareStruct = structs.BoardSquare;

public class ChessBoard : MonoBehaviour
{
    [SerializeField] private ChessPiece bishopLight;
    [SerializeField] private ChessPiece bishopDark;
    [SerializeField] private ChessPiece kingLight;
    [SerializeField] private ChessPiece kingDark;
    [SerializeField] private ChessPiece knightLight;
    [SerializeField] private ChessPiece knightDark;
    [SerializeField] private ChessPiece pawnLight;
    [SerializeField] private ChessPiece pawnDark;
    [SerializeField] private ChessPiece queenLight;
    [SerializeField] private ChessPiece queenDark;
    [SerializeField] private ChessPiece rookLight;
    [SerializeField] private ChessPiece rookDark;
    [SerializeField] private BoardSquareVisual boardSquareVisual;

    public static readonly string[] XAxisValues = new string[] {"a", "b", "c", "d", "e", "f", "g", "h"};
    public static readonly string[] ZAxisValues = new string[] {"1", "2", "3", "4", "5", "6", "7", "8"};

    public static List<BoardSquareStruct> startingPositions = new List<BoardSquareStruct>();
    public static GridXZ<BoardSquare> Grid;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        float cellSize = 10f;
        Grid = new GridXZ<BoardSquare>(XAxisValues, ZAxisValues, cellSize, Vector3.zero, boardSquareVisual,
            (GridXZ<BoardSquare> g, string x, string z, BoardSquareVisual bsv) =>
                new BoardSquare(g, x, z, bsv));
    }

    private void Start()
    {
        InitializeBoardSquares();
        InitializeChessPieces();
    }

    public static List<XZCoordinate> AvailableMoves(List<List<XZCoordinate>> possibleMoves, ChessPiece chessPiece)
    {
        var availableMoves = new List<XZCoordinate>();

        foreach (var coordinateList in possibleMoves)
        {
            foreach (var coordinate in coordinateList)
            {
                BoardSquare boardSquare = Grid.GetGridObject(coordinate.X, coordinate.Z);
                if (!boardSquare.ChessPiece)
                {
                    availableMoves.Add(coordinate);
                }
                else
                {
                    if (boardSquare.ChessPiece.isLight != chessPiece.isLight) availableMoves.Add(coordinate);
                    break;
                }
            }
        }

        var showDebug = false;
        if (showDebug)
        {
            foreach (var coordinate in availableMoves)
            {
                Debug.Log(coordinate.X + " , " + coordinate.Z);
            }
        }

        return availableMoves;
    }

    private void HighlightBoardSquares(List<XZCoordinate> coordinates)
    {
        foreach (var coordinate in coordinates)
        {
            Debug.Log(coordinate.X + " , " + coordinate.Z);
            BoardSquare boardSquare = Grid.GetGridObject(coordinate.X, coordinate.Z);
            Debug.Log(boardSquare.ToString());
        }
    }

    private void InitializeChessPieces()
    {
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(0, 0), rookLight));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(7, 0), rookLight));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(1, 0), knightLight));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(6, 0), knightLight));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(2, 0), bishopLight));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(5, 0), bishopLight));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(4, 0), queenLight));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(3, 0), kingLight));
        for (int i = 0; i < XAxisValues.Length; i++)
        {
            startingPositions.Add(new BoardSquareStruct(new XZCoordinate(i, 1), pawnLight));
        }

        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(0, 7), rookDark));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(7, 7), rookDark));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(1, 7), knightDark));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(6, 7), knightDark));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(2, 7), bishopDark));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(5, 7), bishopDark));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(4, 7), queenDark));
        startingPositions.Add(new BoardSquareStruct(new XZCoordinate(3, 7), kingDark));
        for (int i = 0; i < XAxisValues.Length; i++)
        {
            startingPositions.Add(new BoardSquareStruct(new XZCoordinate(i, 6), pawnDark));
        }

        foreach (var boardSquare in startingPositions)
        {
            var x = boardSquare.Coordinate.X;
            var z = boardSquare.Coordinate.Z;
            BoardSquare square = Grid.GetGridObject(x, z);
            var chessPiece = Instantiate(boardSquare.ChessPiece, Grid.GetWorldPosition(x, z), Quaternion.identity);
            square.SetChessPiece(chessPiece);
            square.ChessPiece.boardPosition = square;
        }
    }

    private void InitializeBoardSquares()
    {
        for (int x = 0; x < XAxisValues.Length; x++)
        {
            for (int z = 0; z < ZAxisValues.Length; z++)
            {
                var color = (x + z) % 2 == 0 ? BoardSquareVisual.SquareColor.Dark : BoardSquareVisual.SquareColor.Light;
                BoardSquare square = Grid.GetGridObject(x, z);
                var boardSquareVisual = Instantiate(square.BoardSquareVisual, Grid.GetWorldPosition(x, z) + new Vector3(10f, 0.2f, 10f) * .5f,
                    Quaternion.Euler(90f, 0,0));
                boardSquareVisual.transform.parent = _transform;
                boardSquareVisual.SetColor(color);
            }
        }
    }
}