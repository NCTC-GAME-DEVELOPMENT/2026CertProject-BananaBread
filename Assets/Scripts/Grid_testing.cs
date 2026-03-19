using UnityEngine;

public class Grid_testing : MonoBehaviour
{
    public Grid grid;
    public int width;
    public int height;




    private void Start()
    {
        grid = new Grid(width, height, 10f, new Vector3(20, 0));
    }

  

}
