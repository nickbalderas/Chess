using System.Collections.Generic;
using structs;
using UnityEngine;
using BoardSquareStruct = structs.BoardSquare;

public class ChessPieceFactory : MonoBehaviour
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

    public enum ChessPieceID
    {
        PawnLight,
        PawnDark,
        RookLight,
        RookDark,
        KnightLight,
        KnightDark,
        BishopLight,
        BishopDark,
        QueenLight,
        QueenDark,
        KingLight,
        KingDark
    }

    private static readonly Dictionary<ChessPieceID, ChessPiece> ChessPieceCatalog =
        new Dictionary<ChessPieceID, ChessPiece>();

    private void Awake()
    {
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.PawnLight))
            ChessPieceCatalog.Add(ChessPieceID.PawnLight, pawnLight);
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.PawnDark))
            ChessPieceCatalog.Add(ChessPieceID.PawnDark, pawnDark);
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.RookLight))
            ChessPieceCatalog.Add(ChessPieceID.RookLight, rookLight);
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.RookDark))
            ChessPieceCatalog.Add(ChessPieceID.RookDark, rookDark);
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.KnightLight))
            ChessPieceCatalog.Add(ChessPieceID.KnightLight, knightLight);
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.KnightDark))
            ChessPieceCatalog.Add(ChessPieceID.KnightDark, knightDark);
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.BishopLight))
            ChessPieceCatalog.Add(ChessPieceID.BishopLight, bishopLight);
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.BishopDark))
            ChessPieceCatalog.Add(ChessPieceID.BishopDark, bishopDark);
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.QueenLight))
            ChessPieceCatalog.Add(ChessPieceID.QueenLight, queenLight);
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.QueenDark))
            ChessPieceCatalog.Add(ChessPieceID.QueenDark, queenDark);
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.KingLight))
            ChessPieceCatalog.Add(ChessPieceID.KingLight, kingLight);
        if (!ChessPieceCatalog.ContainsKey(ChessPieceID.KingDark))
            ChessPieceCatalog.Add(ChessPieceID.KingDark, kingDark);
    }

    public static ChessPiece GetChessPiece(ChessPieceID chessPieceID)
    {
        return ChessPieceCatalog[chessPieceID];
    }

    public static List<BoardSquareStruct> IssueStandardSet()
    {
        var standardSet = new List<BoardSquareStruct>
        {
            new BoardSquareStruct(new XZCoordinate(0, 0), GetChessPiece(ChessPieceID.RookLight)),
            new BoardSquareStruct(new XZCoordinate(1, 0), GetChessPiece(ChessPieceID.KnightLight)),
            new BoardSquareStruct(new XZCoordinate(2, 0), GetChessPiece(ChessPieceID.BishopLight)),
            new BoardSquareStruct(new XZCoordinate(3, 0), GetChessPiece(ChessPieceID.QueenLight)),
            new BoardSquareStruct(new XZCoordinate(4, 0), GetChessPiece(ChessPieceID.KingLight)),
            new BoardSquareStruct(new XZCoordinate(5, 0), GetChessPiece(ChessPieceID.BishopLight)),
            new BoardSquareStruct(new XZCoordinate(6, 0), GetChessPiece(ChessPieceID.KnightLight)),
            new BoardSquareStruct(new XZCoordinate(7, 0), GetChessPiece(ChessPieceID.RookLight)),

            new BoardSquareStruct(new XZCoordinate(0, 7), GetChessPiece(ChessPieceID.RookDark)),
            new BoardSquareStruct(new XZCoordinate(1, 7), GetChessPiece(ChessPieceID.KnightDark)),
            new BoardSquareStruct(new XZCoordinate(2, 7), GetChessPiece(ChessPieceID.BishopDark)),
            new BoardSquareStruct(new XZCoordinate(3, 7), GetChessPiece(ChessPieceID.QueenDark)),
            new BoardSquareStruct(new XZCoordinate(4, 7), GetChessPiece(ChessPieceID.KingDark)),
            new BoardSquareStruct(new XZCoordinate(5, 7), GetChessPiece(ChessPieceID.BishopDark)),
            new BoardSquareStruct(new XZCoordinate(6, 7), GetChessPiece(ChessPieceID.KnightDark)),
            new BoardSquareStruct(new XZCoordinate(7, 7), GetChessPiece(ChessPieceID.RookDark))
        };
        for (var i = 0; i < 8; i++)
        {
            standardSet.Add(new BoardSquareStruct(new XZCoordinate(i, 1), GetChessPiece(ChessPieceID.PawnLight)));
            standardSet.Add(new BoardSquareStruct(new XZCoordinate(i, 6), GetChessPiece(ChessPieceID.PawnDark)));
        }

        return standardSet;
    }
}