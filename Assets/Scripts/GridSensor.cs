using UnityEngine;

public class GridSensor : MonoBehaviour
{
    int PosX;
    int PosY;
    public Grid_testing gt;
    Collider volume;
    void Start()
    {
        //Need a way to find this scene's Grid_testing script
        volume = gameObject.GetComponent<BoxCollider>();
    }

    public void SetValues(int x, int y)
    {
        PosX = x;
        PosY = y;
    }

    private void OnTriggerEnter(Collider other)
    {
        Obstacle wall = other.GetComponent<Obstacle>();
        PlayerController pc = other.GetComponent<PlayerController>();
        //Ghost ghost =  other.GetComponent<Ghost>();

        if (wall)
        {
            gt.grid.SetValue(PosX, PosY, (1));
            Destroy(this);
        }

        if (pc && gt.grid.GetValue(PosX, (PosY + 1)) == 0)
        {
            gt.grid.SetValue(PosX, PosY, (3));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();

        if (pc)
        {
           gt.grid.SetValue(PosX, PosY, (0));
        }
    }
}
