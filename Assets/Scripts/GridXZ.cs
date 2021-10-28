using System;
using UnityEngine;

public class GridXZ<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;

    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public string X;
        public string Z;
    }

    private readonly string[] _zAxisValues;
    private readonly string[] _xAxisValues;
    
    private readonly float _cellSize;
    private readonly Vector3 _originPosition;
    private readonly TGridObject[,] _gridArray;

    public GridXZ(string[] xAxisValues, string[] zAxisValues, float cellSize, Vector3 originPosition, Func<GridXZ<TGridObject>, string, string, TGridObject> createGridObject)
    {
        _xAxisValues = xAxisValues;
        _zAxisValues = zAxisValues;
        _cellSize = cellSize;
        _originPosition = originPosition;

        _gridArray = new TGridObject[xAxisValues.Length, zAxisValues.Length];
        
        for (int x = 0; x < xAxisValues.Length; x++)
        {
            for (int i = 0; i < zAxisValues.Length; i++)
            {
                _gridArray[x, i] = createGridObject(this, xAxisValues[x], zAxisValues[i]);
            }
        }

        bool showDebug = false;
        if (showDebug)
        {
            TextMesh[][] debugTextArray = new TextMesh[xAxisValues.Length][];
            for (int index = 0; index < xAxisValues.Length; index++)
            {
                debugTextArray[index] = new TextMesh[zAxisValues.Length];
            }

            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int i = 0; i < _gridArray.GetLength(1); i++)
                {
                    debugTextArray[x][i] = CreateWorldText(null, _gridArray[x, i]?.ToString(),
                        GetWorldPosition(x, i) + new Vector3(cellSize, 0, _cellSize) * .5f, 20, Color.black,
                        TextAnchor.MiddleCenter, TextAlignment.Left, 5000);
                }
            }

            OnGridObjectChanged += (sender, eventArgs) =>
            {
                var xIndex = Array.IndexOf(xAxisValues, eventArgs.X);
                var zIndex = Array.IndexOf(zAxisValues, eventArgs.Z);
                debugTextArray[xIndex][zIndex].text = _gridArray[xIndex, zIndex].ToString();
            };
        }
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0,  z) * _cellSize + _originPosition;
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        z = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
    }

    private void SetGridObject(int x, int z, TGridObject value)
    {
        if (x >= 0 && z >= 0 && x < _xAxisValues.Length && z < _zAxisValues.Length)
        {
            _gridArray[x, z] = value;
        }
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        GetXZ(worldPosition, out var x, out var z);
        SetGridObject(x, z, value);
    }

    public void TriggerGridObjectChanged(string x, string z)
    {
        if (OnGridObjectChanged != null)
        {
            OnGridObjectChanged(this, new OnGridObjectChangedEventArgs {X = x, Z = z});
        }
    }

    public TGridObject GetGridObject(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < _xAxisValues.Length && z < _zAxisValues.Length)
        {
            return _gridArray[x, z];
        }

        return default(TGridObject);
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        GetXZ(worldPosition, out var x, out var z);
        return GetGridObject(x, z);
    }

    private static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize,
        Color color,
        TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}