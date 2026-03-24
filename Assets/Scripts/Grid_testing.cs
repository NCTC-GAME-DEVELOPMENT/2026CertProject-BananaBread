using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Grid_testing : MonoBehaviour
{
    public Grid grid;
    public int width;
    public int height;
    public float cellSize = 10.0f;
    public Vector3 origin;

    private void Awake()
    {
        grid = new Grid(width, height, cellSize, origin);
        grid.AddValue(2, 1,  (2));
    }

}
