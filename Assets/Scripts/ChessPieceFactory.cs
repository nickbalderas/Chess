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

    private static readonly Dictionary<ChessPieceID, ChessPiece> ChessPieceCatalog = new Dictionary<ChessPieceID, ChessPiece>();

    private void Awake()
    {
        ChessPieceCatalog.Add(ChessPieceID.PawnLight, pawnLight);
        ChessPieceCatalog.Add(ChessPieceID.PawnDark, pawnDark);
        ChessPieceCatalog.Add(ChessPieceID.RookLight, rookLight);
        ChessPieceCatalog.Add(ChessPieceID.RookDark, rookDark);
        ChessPieceCatalog.Add(ChessPieceID.KnightLight, knightLight);
        ChessPieceCatalog.Add(ChessPieceID.KnightDark, knightDark);
        ChessPieceCatalog.Add(ChessPieceID.BishopLight, bishopLight);
        ChessPieceCatalog.Add(ChessPieceID.BishopDark, bishopDark);
        ChessPieceCatalog.Add(ChessPieceID.QueenLight, queenLight);
        ChessPieceCatalog.Add(ChessPieceID.QueenDark, queenDark);
        ChessPieceCatalog.Add(ChessPieceID.KingLight, kingLight);
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
