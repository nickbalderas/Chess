using UnityEngine;

public class BoardSquareVisual : MonoBehaviour
{
    public enum SquareColor
    {
        Light,
        Dark
    }
    
    public Material lightMaterial;
    public Material darkMaterial;

    public void SetColor(SquareColor color)
    {
        GetComponent<Renderer>().material = color == SquareColor.Light
            ? lightMaterial
            : darkMaterial;
    }
}