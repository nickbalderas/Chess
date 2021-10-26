using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid grid;
    public Camera camera;
    
    void Start()
    {
        grid = new Grid(4, 2, 10f, new Vector3 (20,0));
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue))
        {
            grid.SetValue(raycastHit.point, 56);
            Debug.Log(grid.GetValue(raycastHit.point));
        }
        
    }
}
