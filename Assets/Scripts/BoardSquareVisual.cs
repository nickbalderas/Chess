using UnityEngine;

public class BoardSquareVisual : MonoBehaviour
{
    public enum SquareColor
    {
        Light,
        Dark,
        Highlight,
        EnemyHighlight
    }

    private Outline _highlight;
    public Material lightMaterial;
    public Material darkMaterial;
    public Material highlightMaterial;
    public Material enemyHighlight;

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
            case SquareColor.EnemyHighlight:
                GetComponent<Renderer>().material = enemyHighlight;
                break;
        }
    }

    public void Highlight(bool indicator, bool isEnemy = false)
    {
        if (indicator && _highlight.enabled) return;
        if (!indicator && _highlight.enabled)
        {
            _highlight.enabled = false;
            SetColor(_selectedColor);
        }
        if (indicator && !_highlight.enabled)
        {
            SetColor(isEnemy ? SquareColor.EnemyHighlight : SquareColor.Highlight);
            _highlight.enabled = true;
            _highlight.OutlineMode = Outline.Mode.OutlineAll;
            _highlight.OutlineColor = Color.blue;
            _highlight.OutlineWidth = 10f;
        }
    }
}