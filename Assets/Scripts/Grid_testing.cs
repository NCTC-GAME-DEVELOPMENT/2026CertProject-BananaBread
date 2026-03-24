using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Grid_testing : MonoBehaviour
{
    public Grid grid;
    public int width;
    public int height;
    public float cellSize = 10.0f;
    public Vector3 origin;

    private void Start()
    {
        // this is just so we have a referens for the grid in the scene
        // will be replaced later... hopefully
        grid.AddValue(0, 1, (1));
        grid.AddValue(0, 4, (1));
        grid.AddValue(5, 1, (6));
        grid.AddValue(5, 3, (2));
        grid.AddValue(6, 2, (2));
        grid.AddValue(6, 3, (2));
        grid.AddValue(5, 2, (2));
        grid.AddValue(4, 2, (2));
        grid.AddValue(4, 3, (2));
        grid.AddValue(7, 3, (2));
        grid.AddValue(7, 2, (2));
        grid.AddValue(10, 4, (7));
    }

    private void Awake()
    {
        grid = new Grid(width, height, cellSize, origin);
    }

}
