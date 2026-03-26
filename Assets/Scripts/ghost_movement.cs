using UnityEngine;
using UnityEngine.UIElements;

public class ghost_movement : MonoBehaviour
{
    public Grid_testing gt;
    public int PosX;
    public int PosY;
    Vector3 myPosition;
    public int down, up, left, right;


    public void Start()
    {
        gt.grid.SetValue(PosX, PosY, (7));
        myPosition = gameObject.transform.position;
    }
    public void Update()
    {
      down =  gt.grid.GetValue(PosX - 1, PosY);
       up = gt.grid.GetValue(PosX + 1, PosY);
       left = gt.grid.GetValue(PosX, PosY - 1);
       right = gt.grid.GetValue(PosX, PosY + 1);
        if ((down == 1 && up == 1) || (down == 1 && left == 1) || (down == 1 && right == 1) || (up == 1 && left == 1) || (up == 1 && right == 1) || (left == 1 && right == 1) || (down == 1) || (up == 1) || (left == 1) || (right == 1))
        {
            // move to any space with 1
        }
        if ((down == 2) || (up == 2) || (left == 2) || (right == 2))
        {
            //temporary exsclue spot
        }
        if (down == 0 &&  up == 0 && left == 0 && right == 0)
        {
            // move randomly
        }
       
       
    }
}