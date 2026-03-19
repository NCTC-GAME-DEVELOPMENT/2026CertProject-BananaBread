using UnityEngine;

public class Grid_testing : MonoBehaviour
{
    public Grid grid;
    public int width;
    public int height;
    public float cellSize = 10.0f;
    public Vector3 origin;

    private void Start()
    {
        grid = new Grid(width, height, cellSize, origin);
    }

  

}
