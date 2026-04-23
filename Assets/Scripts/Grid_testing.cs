using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Grid_testing : MonoBehaviour
{
    public Grid grid;
    public int width;
    public int height;
    public float cellSize = 10.0f;
    public Vector3 origin;
    public bool areDebugNumbersVisible = false;

    private void Start()
    {
        //Value Key:
        //0: Open Space
        //1: Immobile Obstacle
        //2: Crate / Objective Crate
        //3: Player
        //4: Ghost

        grid.setDebugVisibility(areDebugNumbersVisible);

        Debug.Log(grid.GetValue(0, 1));
    }

    private void Awake()
    {
        grid = new Grid(width, height, cellSize, origin);
    }

}
