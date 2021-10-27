using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [SerializeField] private ChessPiece chessPiece;
    
    public static readonly string[] XAxisValues = new string[] {"a", "b", "c", "d", "e", "f", "g", "h"};
    public static readonly string[] ZAxisValues = new string[] { "1", "2", "3", "4", "5", "6", "7", "8" };
    private GridXZ<BoardSquare> _grid;

    private void Awake()
    {
        float cellSize = 10f;
        _grid = new GridXZ<BoardSquare>(XAxisValues, ZAxisValues, cellSize, Vector3.zero,
            (GridXZ<BoardSquare> g, string x, string z) => new BoardSquare(g, x, z));
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var raycastHit, float.MaxValue)) return;
        _grid.GetXZ(raycastHit.point, out var x, out var z);
    
        BoardSquare boardSquare = _grid.GetGridObject(x, z);
        if (!boardSquare.CanBuild()) return;
       
        chessPiece = Instantiate(chessPiece, _grid.GetWorldPosition(x,z), Quaternion.identity);
        boardSquare.SetChessPiece(chessPiece);
        boardSquare.ChessPiece.boardPosition = boardSquare;

        foreach (var coordinate in boardSquare.ChessPiece.GetPossibleMoves())
        {
            Debug.Log(coordinate.ToString());
        }
    }
}
