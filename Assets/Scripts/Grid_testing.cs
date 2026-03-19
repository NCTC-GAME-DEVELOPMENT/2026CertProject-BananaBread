using UnityEngine;

public class Grid_testing : MonoBehaviour
{
    public Grid grid;
    public int width;
    public int height;
    public float cellSize = 10.0f;

    private void Start()
    {
        grid = new Grid(width, height, cellSize, new Vector3(20, 0));
    }

  

}
