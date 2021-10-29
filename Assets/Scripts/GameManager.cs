using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ChessPiece SelectedChessPiece { get; private set; }

    public void SetSelectedChessPiece(ChessPiece chessPiece)
    {
        RemoveSelectedChessPiece();
        SelectedChessPiece = chessPiece;
        SelectedChessPiece.Selected();
    }

    public void RemoveSelectedChessPiece()
    {
        if (!SelectedChessPiece) return;
        SelectedChessPiece.Unselected();
        SelectedChessPiece = null;
    }
}