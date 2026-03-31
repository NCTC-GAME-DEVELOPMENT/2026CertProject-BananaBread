using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class ghost_movement : MonoBehaviour
{
    public Grid_testing gt;
    public int XStuf;
    public int ZStuf;
    Vector3 myPosition;
    private int down, up, left, right;
    private Rigidbody rb;
    public float speed = 1.0f;
    int randomMovement;
    public void Start()
    {
        gt.grid.SetValue(XStuf, ZStuf, (7));
        myPosition = gameObject.transform.position;
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
      down =  gt.grid.GetValue(XStuf, ZStuf - 1);
       up = gt.grid.GetValue(XStuf, ZStuf + 1);
       left = gt.grid.GetValue(XStuf - 1, ZStuf);
       right = gt.grid.GetValue(XStuf + 1, ZStuf);
        if ((down == 1 && up == 1) || (down == 1 && left == 1) || (down == 1 && right == 1) || (up == 1 && left == 1) || (up == 1 && right == 1) || (left == 1 && right == 1) || (down == 1) || (up == 1) || (left == 1) || (right == 1))
        {
            if (down == 1)
            {
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf, ZStuf - 1, (7));
                ZStuf = ZStuf - 1;
            }
            if (up == 1)
            {
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf, ZStuf + 1, (7));
                ZStuf = ZStuf + 1;
            }
            if (left == 1)
            {
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf - 1, ZStuf, (7));
                XStuf = XStuf - 1;
            }
            if (right == 1)
            {
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf + 1, ZStuf, (7));
                XStuf = XStuf + 1;
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
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf + 1, ZStuf, (7));
                XStuf = XStuf + 1;
            }
            if ((down == 2) && (up == 2) && (right == 2))
            {
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf - 1, ZStuf, (7));
                XStuf = XStuf - 1;
            }
            if ((down == 2) && (right == 2) && (left == 2))
            {
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf, ZStuf + 1, (7));
                ZStuf = ZStuf + 1;
            }
            if ((right == 2) && (up == 2) && (left == 2))
            {
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf, ZStuf - 1, (7));
                ZStuf = ZStuf - 1;
            }
            if ((down == 2) && (up == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf - 1, ZStuf, (7));
                    XStuf = XStuf - 1;
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf + 1, ZStuf, (7));
                    XStuf = XStuf + 1;
                }

            }
            if ((down == 2) && (left == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf + 1, (7));
                    ZStuf = ZStuf + 1;
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf + 1, ZStuf, (7));
                    XStuf = XStuf + 1;
                }
            }
            if ((down == 2) &&  (right == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf + 1, (7));
                    ZStuf = ZStuf + 1;
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf - 1, ZStuf, (7));
                    XStuf = XStuf - 1;
                }
            }
            if ((up == 2) && (left == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf - 1, (7));
                    ZStuf = ZStuf - 1;
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf + 1, ZStuf, (7));
                    XStuf = XStuf + 1;
                }
            }
            if ((up == 2) && (right == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf - 1, (7));
                    ZStuf = ZStuf - 1;
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf - 1, ZStuf, (7));
                    XStuf = XStuf - 1;
                }
            }
            if ((left == 2) && (right == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf - 1, (7));
                    ZStuf = ZStuf - 1;
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf + 1, (7));
                    ZStuf = ZStuf + 1;
                }
            }
            if ( down == 2 )
            {
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf + 1, (7));
                    ZStuf = ZStuf + 1;
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf - 1, ZStuf, (7));
                    XStuf = XStuf - 1;
                }
                if (randomMovement == 3)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf + 1, ZStuf, (7));
                    XStuf = XStuf + 1;
                }
            }
            if (up== 2)
            {
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf - 1, (7));
                    ZStuf = ZStuf - 1;
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf - 1, ZStuf, (7));
                    XStuf = XStuf - 1;
                }
                if (randomMovement == 3)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf + 1, ZStuf, (7));
                    XStuf = XStuf + 1;
                }
            }
            if (left == 2)
            {
                Debug.Log("left wall");
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf + 1, (7));
                    ZStuf = ZStuf + 1;
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf - 1, (7));
                    ZStuf = ZStuf - 1;
                }
                if (randomMovement == 3)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf + 1, ZStuf, (7));
                    XStuf = XStuf + 1;
                }
            }
            if (right == 2)
            {
                Debug.Log("right wall");
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf + 1, (7));
                    ZStuf = ZStuf + 1;
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf - 1, ZStuf, (7));
                    XStuf = XStuf - 1;
                }
                if (randomMovement == 3)
                {
                    gt.grid.SetValue(XStuf, ZStuf, (0));
                    gt.grid.SetValue(XStuf, ZStuf - 1, (7));
                    ZStuf = ZStuf - 1;
                }
            }

        }
        if (down == 0 &&  up == 0 && left == 0 && right == 0)
        {
             randomMovement = Random.Range(1, 4);
            
            if (randomMovement == 1)
            {
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf, ZStuf - 1, (7));
                ZStuf = ZStuf - 1;
            }
            if (randomMovement == 2)
            {
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf, ZStuf + 1, (7));
                ZStuf = ZStuf + 1;
            }
            if (randomMovement == 3)
            {
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf - 1, ZStuf, (7));
                XStuf = XStuf - 1;
            }
            if (randomMovement == 4)
            {
                gt.grid.SetValue(XStuf, ZStuf, (0));
                gt.grid.SetValue(XStuf + 1, ZStuf, (7));
                XStuf = XStuf + 1;
            }
        }
        else
        {
            Debug.Log("Not Accounted for");
        }
       
       
    }
}