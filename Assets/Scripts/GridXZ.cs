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

    public GridXZ(string[] xAxisValues, string[] zAxisValues, float cellSize, Vector3 originPosition, BoardSquareVisual boardSquareVisual,
        Func<GridXZ<TGridObject>, string, string, BoardSquareVisual, TGridObject> createGridObject)
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
                _gridArray[x, i] = createGridObject(this, xAxisValues[x], zAxisValues[i], boardSquareVisual);
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * _cellSize + _originPosition;
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
}