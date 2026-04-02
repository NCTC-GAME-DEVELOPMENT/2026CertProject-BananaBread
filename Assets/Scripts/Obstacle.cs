using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Grid_testing gt;

    private int PosX;
    private int PosY;

    private void Start()
    {
        gt = GameObject.Find("GameManager").GetComponent<Grid_testing>();

        FindStartCoordinates();
        gt.grid.SetValue(PosX, PosY, (1));
    }

    //Uses the object's world position to set its starting coordinates
    public void FindStartCoordinates()
    {
        Vector3 pos = gameObject.transform.position;

        //Find the Position of the X Variable
        float x = (((pos.x - 1.5f) / 3) + (gt.width / 2));
        PosX = (int)x;

        //Do the same for the Y Variable
        float y = (((pos.z - 1.5f) / 3) + (gt.height / 2));
        PosY = (int)y;
    }
}
