using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Grid_testing : MonoBehaviour
{
    public Grid grid;
    public int width;
    public int height;
    public float cellSize = 10.0f;
    public Vector3 origin;
    public GameObject sensor;

    private void Start()
    {
        //Now that everything but the ghosts set themselves, these placeholder values are no longer needed.

        //Value Key:
        //0: Open Space
        //1: Immobile Obstacle
        //2: Crate / Objective Crate
        //3: Player
        //4: Ghost

        //grid.AddValue(5, 1, (6));
        //grid.AddValue(5, 3, (2));
        //grid.AddValue(6, 2, (2));
        //grid.AddValue(6, 3, (2));
        //grid.AddValue(5, 2, (2));
        //grid.AddValue(4, 2, (2));
        //grid.AddValue(4, 3, (2));
        //grid.AddValue(7, 3, (2));
        //grid.AddValue(7, 2, (2));

        //this is a way to use GetValue to get the stored value
        Debug.Log(grid.GetValue(0, 1));
    }

    private void Awake()
    {
        grid = new Grid(width, height, cellSize, origin, sensor);
    }

}
