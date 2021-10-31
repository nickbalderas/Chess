using System.Collections.Generic;
using structs;

public class Player
{
    public Player OpposingPlayer;
    private readonly List<ChessPiece> _chessPieces;
    public readonly List<ChessPiece> EliminatedPieces;
    private readonly List<XZCoordinate> _moveHistory;
    public readonly bool IsAssignedLight;
    // private bool _isInCheck;
    // private bool _isInCheckMate;

    public Player(List<ChessPiece> chessPieces, List<ChessPiece> eliminatedPieces, List<XZCoordinate> moveHistory, bool isAssignedLight)
    {
        _chessPieces = chessPieces;
        EliminatedPieces = eliminatedPieces;
        IsAssignedLight = isAssignedLight;
        _moveHistory = moveHistory;
        // _isInCheck = false;
        // _isInCheckMate = false;
    }

    public void AddMoveHistory(XZCoordinate move)
    {
        _moveHistory.Add(move);
    }

    public void AddToPossession(ChessPiece chessPiece)
    {
        chessPiece.AssignedPlayer = this;
        if (chessPiece.isEliminated) EliminatedPieces.Add(chessPiece);
        else _chessPieces.Add(chessPiece);
    }

    public void RemoveFromPossession(ChessPiece chessPiece)
    {
        if (chessPiece.isEliminated)
        {
            EliminatedPieces.Remove(chessPiece);
            chessPiece.isEliminated = false;
        }
        else
        {
            _chessPieces.Remove(chessPiece);
            chessPiece.isEliminated = true;
        }
        OpposingPlayer.AddToPossession(chessPiece);
    }
}
