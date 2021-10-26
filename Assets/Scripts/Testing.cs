using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid<TestGridObject> _grid;
    public new Camera camera;
    
    void Start()
    {
        _grid = new Grid<TestGridObject>(4, 2, 10f, Vector3.zero,
            (g, x, y) => new TestGridObject(g, x, y));
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (!Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue)) return;
        var testGridObject = _grid.GetGridObject(raycastHit.point);
        testGridObject?.AddValue(5);
    }
}

//  This would be ChessPiece for your chess game
public class TestGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;

    private readonly Grid<TestGridObject> _grid;
    private readonly int _x;
    private readonly int _y;
    private int _value;

    public TestGridObject(Grid<TestGridObject> grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
    }

    public void AddValue(int addValue)
    {
        _value += addValue;
        Mathf.Clamp(_value, MIN, MAX);
        _grid.TriggerGridObjectChanged(_x, _y);
    }

    public float GetValueNormalized()
    {
        return (float) _value / MAX;
    }

    public override string ToString()
    {
        return _value.ToString();
    }
}