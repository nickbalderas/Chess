using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private Transform testTransform;
    
    private GridXZ<GridObject> _grid;

    private void Awake()
    {
        var xValues = new string[] {};
        var zValues = new string[] {};
        float cellSize = 10f;
        _grid = new GridXZ<GridObject>(xValues, zValues, cellSize, Vector3.zero,
            (GridXZ<GridObject> g, string x, string z) => new GridObject(g, x, z));
    }
    
    public class GridObject
    {
        private GridXZ<GridObject> _grid;
        private string _x;
        private string _z;
        private Transform transform;
    
        public GridObject(GridXZ<GridObject> grid, string x, string z)
        {
            _grid = grid;
            _x = x;
            _z = z;
        }
    
        public void SetTransform(Transform transform)
        {
            this.transform = transform;
            _grid.TriggerGridObjectChanged(_x, _z);
        }
    
        public void ClearTransform()
        {
            this.transform = null;
            _grid.TriggerGridObjectChanged(_x, _z);
        }
    
        public bool CanBuild()
        {
            return transform == null;
        }
    
        public override string ToString()
        {
            return _x + " , " + _z + "\n" + transform;
        }
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var raycastHit, float.MaxValue)) return;
        _grid.GetXZ(raycastHit.point, out var x, out var z);
    
        GridObject gridObject = _grid.GetGridObject(x, z);
        if (!gridObject.CanBuild()) return;
       
        Transform builtTransform = Instantiate(testTransform, _grid.GetWorldPosition(x,z), Quaternion.identity);
        gridObject.SetTransform(builtTransform);
    }
}
