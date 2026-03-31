using UnityEngine;
using UnityEngine.UIElements;

public class ghost_movement : MonoBehaviour
{
    public Grid_testing gt;
    public int PosX;
    public int PosY;
    Vector3 myPosition;
    public int down, up, left, right;
    private Rigidbody rb;
    

    public void Start()
    {
        gt.grid.SetValue(PosX, PosY, (7));
        myPosition = gameObject.transform.position;
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
      down =  gt.grid.GetValue(PosX - 1, PosY);
       up = gt.grid.GetValue(PosX + 1, PosY);
       left = gt.grid.GetValue(PosX, PosY - 1);
       right = gt.grid.GetValue(PosX, PosY + 1);
        if ((down == 1 && up == 1) || (down == 1 && left == 1) || (down == 1 && right == 1) || (up == 1 && left == 1) || (up == 1 && right == 1) || (left == 1 && right == 1) || (down == 1) || (up == 1) || (left == 1) || (right == 1))
        {
            if (down == 1)
            {
                // move down and freeze player
            }
            if (up == 1)
            {
                // move up and freeze player
            }
            if (left == 1)
            {
                // move left and freeze player
            }
            if (right == 1)
            {
                // move right and freeze player
            }
        }
        if ((down == 2) || (up == 2) || (left == 2) || (right == 2))
        {
            if ((down == 2) && (up == 2) && (left == 2) && (right == 2))
            {
                Debug.Log("Ghost Trapped");
            }
            if ((down == 2) && (up == 2) && (left == 2))
            {
                // restrict movement to right
            }
            if ((down == 2) && (up == 2) && (right == 2))
            {
                // restrict movement to left
            }
            if ((down == 2) && (right == 2) && (left == 2))
            {
                // restrict movement to up
            }
            if ((right == 2) && (up == 2) && (left == 2))
            {
                // restrict movement to down
            }
            if ((down == 2) && (up == 2))
            {
                //restrict movement to left and right
            }
            if ((down == 2) && (left == 2))
            {
                // restrict movement to up and right
            }
            if ((down == 2) &&  (right == 2))
            {
                // restrict movement to up and left
            }
            if ((up == 2) && (left == 2))
            {
                //restrict moevment to down and right
            }
            if ((up == 2) && (right == 2))
            {
                //restrict movement to down and left
            }
            if ((left == 2) && (right == 2))
            {
                //restrict movement to up and down
            }
            if ( down == 2 )
            {
                //restrict movement to up,left and right
            }
            if (up== 2)
            {
                //restrict movement to down,left and right
            }
            if (left == 2)
            {
                //restrict movement to up,down and right
            }
            if (right == 2)
            {
                //restrict movement to up,left and down
            }

        }
        if (down == 0 &&  up == 0 && left == 0 && right == 0)
        {
            // move randomly
        }
        else
        {
            Debug.Log("Not Accounted for");
        }
       
       
    }
}