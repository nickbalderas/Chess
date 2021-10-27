using UnityEngine;

[CreateAssetMenu(fileName = "ChessPieceScriptableObject", menuName = "ScriptableObjects/ChessPiece")]
public class TestScriptableObject : ScriptableObject
{
    public string name;
    public ChessPiece ChessPiece;
}
