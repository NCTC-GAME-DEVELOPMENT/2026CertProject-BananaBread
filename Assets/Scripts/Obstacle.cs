using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Grid_testing gt;

    public int PosX;
    public int PosY;

    private void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gt = gm.GetComponent<Grid_testing>();

        gt.grid.SetValue(PosX, PosY, (1));
    }
}
