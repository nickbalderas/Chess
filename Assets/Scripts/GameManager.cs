using System.Collections.Generic;
using structs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIController uiController;
    public ChessPiece SelectedChessPiece { get; private set; }
    public Player LightPlayer;
    public Player DarkPlayer;
    
    private void Awake()
    {
        uiController = GetComponent<UIController>();
        
        LightPlayer = new Player(new List<ChessPiece>(), new List<ChessPiece>(), new List<XZCoordinate>(), true);
        DarkPlayer = new Player(new List<ChessPiece>(), new List<ChessPiece>(), new List<XZCoordinate>(), false);
        LightPlayer.OpposingPlayer = DarkPlayer;
        DarkPlayer.OpposingPlayer = LightPlayer;
    }

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