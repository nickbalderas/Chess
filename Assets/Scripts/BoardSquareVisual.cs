using UnityEngine;

public class BoardSquareVisual : MonoBehaviour
{
    public enum SquareColor
    {
        Light,
        Dark,
        Highlight
    }

    private Outline _highlight;
    public Material lightMaterial;
    public Material darkMaterial;
    public Material highlightMaterial;

    private SquareColor _selectedColor;

    private void Awake()
    {
        _highlight = GetComponent<Outline>();
        _highlight.enabled = false;
    }

    public void SetColor(SquareColor color)
    {
        switch (color)
        {
            case SquareColor.Light:
                _selectedColor = SquareColor.Light;
                GetComponent<Renderer>().material = lightMaterial;
                break;
            case SquareColor.Dark:
                _selectedColor = SquareColor.Dark;
                GetComponent<Renderer>().material = darkMaterial;
                break;
            case SquareColor.Highlight:
                GetComponent<Renderer>().material = highlightMaterial;
                break;
        }
    }

    public void Highlight(bool indicator)
    {
        if (indicator && _highlight.enabled) return;
        if (!indicator && _highlight.enabled)
        {
            _highlight.enabled = false;
            SetColor(_selectedColor);
        }
        if (indicator && !_highlight.enabled)
        {
            SetColor(SquareColor.Highlight);
            _highlight.enabled = true;
            _highlight.OutlineMode = Outline.Mode.OutlineAll;
            _highlight.OutlineColor = Color.blue;
            _highlight.OutlineWidth = 10f;
        }
    }
}