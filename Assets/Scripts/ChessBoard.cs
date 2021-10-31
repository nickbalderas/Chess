using System.Collections.Generic;
using structs;
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

    private static readonly List<BoardSquareStruct> StartingPositions = new List<BoardSquareStruct>();
    public static GridXZ<BoardSquare> Grid;

    private Transform _transform;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _transform = GetComponent<Transform>();
        Grid = new GridXZ<BoardSquare>(XAxisValues, ZAxisValues, 10f, Vector3.zero, boardSquareVisual,
            (g, x, z, bsv) => new BoardSquare(g, x, z, bsv));
    }

    private void Start()
    {
        InitializeBoardSquares();
        InitializeChessPieces();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var selectedBoardSquare = HandleSelectedBoardSquare();
        if (selectedBoardSquare == null) return;

        if (_gameManager.SelectedChessPiece && _gameManager.SelectedChessPiece.AvailableMoves.Contains(selectedBoardSquare))
        {
            if (selectedBoardSquare.ChessPiece)
            {
                selectedBoardSquare.ChessPiece.AssignedPlayer.RemoveFromPossession(selectedBoardSquare.ChessPiece);
                selectedBoardSquare.ChessPiece.gameObject.SetActive(false);
            }
            _gameManager.SelectedChessPiece.HandleMovement(Grid, selectedBoardSquare);
            _gameManager.RemoveSelectedChessPiece();
        }
        else
        {
            HandleChessPieceSelection(selectedBoardSquare);
        }
        
    }

    private static BoardSquare HandleSelectedBoardSquare()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var raycastHit, float.MaxValue)) return null;
        Grid.GetXZ(raycastHit.point, out var x, out var z);
        return Grid.GetGridObject(x, z);
    }

    private void HandleChessPieceSelection(BoardSquare selectedBoardSquare)
    {
        var chessPiece = selectedBoardSquare.ChessPiece;
        if (chessPiece == null) return;
        if (!_gameManager.SelectedChessPiece) _gameManager.SetSelectedChessPiece(chessPiece);
        else if (!selectedBoardSquare.IsSelf(_gameManager.SelectedChessPiece.BoardPosition))
            _gameManager.SetSelectedChessPiece(chessPiece);
        else _gameManager.RemoveSelectedChessPiece();
    }

    public static List<BoardSquare> AvailableMoves(List<List<XZCoordinate>> possibleMoves, ChessPiece chessPiece)
    {
        var availableMoves = new List<BoardSquare>();

        foreach (var coordinateList in possibleMoves)
        {
            foreach (var coordinate in coordinateList)
            {
                BoardSquare boardSquare = Grid.GetGridObject(coordinate.X, coordinate.Z);
                if (chessPiece.classification == ChessPiece.PieceType.Pawn)
                {
                    var pawn = (Pawn) chessPiece;
                    foreach (var move in pawn.GetDiagonalMoves())
                    {
                        BoardSquare enemySquare = Grid.GetGridObject(move.X, move.Z);
                        if (enemySquare.ChessPiece && enemySquare.ChessPiece.isLight != pawn.isLight)
                            availableMoves.Add(enemySquare);
                    }
                }

                if (!boardSquare.ChessPiece)
                {
                    availableMoves.Add(boardSquare);
                }
                else
                {
                    if (boardSquare.ChessPiece.isLight != chessPiece.isLight && chessPiece.classification != ChessPiece.PieceType.Pawn) availableMoves.Add(boardSquare);
                    break;
                }
            }
        }

        return availableMoves;
    }

    private void InitializeChessPieces()
    {
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(0, 0), rookLight));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(7, 0), rookLight));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(1, 0), knightLight));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(6, 0), knightLight));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(2, 0), bishopLight));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(5, 0), bishopLight));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(3, 0), queenLight));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(4, 0), kingLight));
        for (int i = 0; i < XAxisValues.Length; i++)
        {
            StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(i, 1), pawnLight));
        }

        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(0, 7), rookDark));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(7, 7), rookDark));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(1, 7), knightDark));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(6, 7), knightDark));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(2, 7), bishopDark));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(5, 7), bishopDark));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(3, 7), queenDark));
        StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(4, 7), kingDark));
        for (int i = 0; i < XAxisValues.Length; i++)
        {
            StartingPositions.Add(new BoardSquareStruct(new XZCoordinate(i, 6), pawnDark));
        }

        foreach (var boardSquare in StartingPositions)
        {
            var x = boardSquare.Coordinate.X;
            var z = boardSquare.Coordinate.Z;
            BoardSquare square = Grid.GetGridObject(x, z);
            var chessPiece = Instantiate(boardSquare.ChessPiece, Grid.GetWorldPosition(x, z), Quaternion.identity);

            if (chessPiece.isLight)
            {
                _gameManager.LightPlayer.AddToPossession(chessPiece);
            }
            else
            {
                _gameManager.DarkPlayer.AddToPossession(chessPiece);
            }
            
            square.SetChessPiece(chessPiece);
            square.ChessPiece.BoardPosition = square;
        }
    }

    private void InitializeBoardSquares()
    {
        for (int x = 0; x < XAxisValues.Length; x++)
        {
            for (int z = 0; z < ZAxisValues.Length; z++)
            {
                var color = (x + z) % 2 == 0 ? BoardSquareVisual.SquareColor.Dark : BoardSquareVisual.SquareColor.Light;
                BoardSquare boardSquare = Grid.GetGridObject(x, z);
                var squareVisual = Instantiate(boardSquare.BoardSquareVisual,
                    Grid.GetWorldPosition(x, z) + new Vector3(10f, 0.2f, 10f) * .5f,
                    Quaternion.Euler(90f, 0, 0));
                boardSquare.SetBoardSquareVisual(squareVisual);
                squareVisual.transform.parent = _transform;
                squareVisual.SetColor(color);
            }
        }
    }
}