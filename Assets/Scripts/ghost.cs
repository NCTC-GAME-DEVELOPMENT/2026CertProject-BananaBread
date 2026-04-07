using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class ghost : Common
{
    
    private int down, up, left, right;
    private Rigidbody rb;
    int randomMovement;
    public int ghostSpeed = 10;
    protected override void Start()
    {
        gt = GameObject.Find("GameManager").GetComponent<Grid_testing>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();

        GridValue = 7;
        StartCoroutine(DelayCoroutine());
        gt.grid.SetValue(PosX, PosY, (GridValue));
        DelayCoroutine();
    }

    IEnumerator DelayCoroutine()
    {
        
        yield return new WaitForSeconds(1);
        Move();
    }

    public void Move()
    {
        down = gt.grid.GetValue(PosX, PosY - 1);
        up = gt.grid.GetValue(PosX, PosY + 1);
        left = gt.grid.GetValue(PosX - 1, PosY);
        right = gt.grid.GetValue(PosX + 1, PosY);
        if ((down == 3 && up == 3) || (down == 3 && left == 3) || (down == 3 && right == 3) || (up == 3 && left == 3) || (up == 3 && right == 3) || (left == 3 && right == 3) || (down == 3) || (up == 3) || (left == 3) || (right == 3))
        {
            if (down == 3)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                PosY = PosY - 1;
                rb.linearVelocity = new Vector3(0, -0, -11);
            }
            if (up == 3)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX, PosY + 1, (GridValue));
                PosY = PosY + 1;
                rb.linearVelocity = new Vector3(0, 0, 11);
            }
            if (left == 3)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX - 1, PosY, (GridValue));
                PosX = PosX - 1;
                rb.linearVelocity = new Vector3(-11, 0, 0);

            }
            if (right == 3)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                PosX = PosX + 1;
                rb.linearVelocity = new Vector3(11, 0, 0);
            }
        }
        if ((down == 1) || (up == 1) || (left == 1) || (right == 1) || (down == 2) || (up == 2) || (left == 2) || (right == 2))
        {
            if ((down == 1) && (up == 1) && (left == 1) && (right == 1) || ((down == 2) & (up == 1) & (left == 1) & (right == 1)) || ((down == 1) & (up == 2) & (left == 1) & (right == 1)) || ((down == 1) & (up == 1) & (left == 2) & (right == 1)) || ((down == 1) & (up == 1) & (left == 1) & (right == 2)))
            {
                Debug.Log("Ghost Trapped");
            }
            if ((down == 1) && (up == 1) && (left == 1))
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                PosX = PosX + 1;
                rb.linearVelocity = new Vector3(11, 0, 0);
            }
            if ((down == 1) && (up == 1) && (right == 1) || (down == 2) && (up == 1) && (right == 1) || (down == 1) && (up == 2) && (right == 1) ||(down == 1) && (up == 1) && (right == 2))
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX - 1, PosY, (GridValue));
                PosX = PosX - 1;
                rb.linearVelocity = new Vector3(-11, 0, 0);

            }
            if ((down == 1) && (right == 1) && (left == 1) || (down == 2) && (right == 1) && (left == 1) || (down == 1) && (right == 2) && (left == 1) || (down == 1) && (right == 1) && (left == 2))
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX, PosY + 1, (GridValue));
                PosY = PosY + 1;
                rb.linearVelocity = new Vector3(0, 0, 11);
            }
            if ((right == 1) && (up == 1) && (left == 1))
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                PosY = PosY - 1;
                rb.linearVelocity = new Vector3(0, -0, -11);
            }
            if ((down == 1) && (up == 1) || (down == 2) && (up == 1) || (down == 1) && (up == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX - 1, PosY, (GridValue));
                    PosX = PosX - 1;
                    rb.linearVelocity = new Vector3(-11, 0, 0);

                }
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                    PosX = PosX + 1;
                    rb.linearVelocity = new Vector3(11, 0, 0);
                }

            }
            if ((down == 1) && (left == 1) || (down == 2) && (left == 1) || (down == 1) && (left == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY + 1, (GridValue));
                    PosY = PosY + 1;
                    rb.linearVelocity = new Vector3(0, 0, 11);
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                    PosX = PosX + 1;
                    rb.linearVelocity = new Vector3(11, 0, 0);
                }
            }
            if ((down == 1) && (right == 1) || (down == 2) && (right == 1) || (down == 1) && (right == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY + 1, (GridValue));
                    PosY = PosY + 1;
                    rb.linearVelocity = new Vector3(0, 0, 11);
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX - 1, PosY, (GridValue));
                    PosX = PosX - 1;
                    rb.linearVelocity = new Vector3(-11, 0, 0);

                }
            }
            if ((up == 1) && (left == 1) || (up == 2) && (left == 1) || (up == 1) && (left == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                    PosY = PosY - 1;
                    rb.linearVelocity = new Vector3(0, -0, -11);
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                    PosX = PosX + 1;
                    rb.linearVelocity = new Vector3(11, 0, 0);
                }
            }
            if ((up == 1) && (right == 1) || (up == 2) && (right == 1) || (up == 1) && (right == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                    PosY = PosY - 1;
                    rb.linearVelocity = new Vector3(0, -0, -11);
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX - 1, PosY, (GridValue));
                    PosX = PosX - 1;
                    rb.linearVelocity = new Vector3(-11, 0, 0);

                }
            }
            if ((left == 1) && (right == 1) || (left == 2) && (right == 1) || (left == 1) && (right == 2))
            {
                randomMovement = Random.Range(1, 2);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                    PosY = PosY - 1;
                    rb.linearVelocity = new Vector3(0, -0, -11);
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY + 1, (GridValue));
                    PosY = PosY + 1;
                    rb.linearVelocity = new Vector3(0, 0, 11);
                }
            }
            if ((down == 1) || (down == 2))
            {
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY + 1, (GridValue));
                    PosY = PosY + 1;
                    rb.linearVelocity = new Vector3(0, 0, 11);
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX - 1, PosY, (7));
                    PosX = PosX - 1;
                    rb.linearVelocity = new Vector3(-11, 0, 0);

                }
                if (randomMovement == 3)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                    PosX = PosX + 1;
                    rb.linearVelocity = new Vector3(11, 0, 0);
                }
            }
            if ((up == 1) || (up == 2))
            {
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                    PosY = PosY - 1;
                    rb.linearVelocity = new Vector3(0, -0, -11);
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX - 1, PosY, (GridValue));
                    PosX = PosX - 1;
                    rb.linearVelocity = new Vector3(-11, 0, 0);

                }
                if (randomMovement == 3)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                    PosX = PosX + 1;
                    rb.linearVelocity = new Vector3(11, 0, 0);
                }
            }
            if ((left == 1) || (left == 2))
            {
                Debug.Log("left wall");
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY + 1, (GridValue));
                    PosY = PosY + 1;
                    rb.linearVelocity = new Vector3(0, 0, 11);
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                    PosY = PosY - 1;
                    rb.linearVelocity = new Vector3(0, -0, -11);
                }
                if (randomMovement == 3)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                    PosX = PosX + 1;
                    rb.linearVelocity = new Vector3(11, 0, 0);
                }
            }
            if ((right == 1) || (right == 2))
            {
                Debug.Log("right wall");
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY + 1, (GridValue));
                    PosY = PosY + 1;
                    rb.linearVelocity = new Vector3(0, 0, 11);
                }
                if (randomMovement == 2)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX - 1, PosY, (GridValue));
                    PosX = PosX - 1;
                    rb.linearVelocity = new Vector3(-11, 0, 0);

                }
                if (randomMovement == 3)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                    PosY = PosY - 1;
                    rb.linearVelocity = new Vector3(0, -0, -11);
                }
            }

        }
        if (down == 0 && up == 0 && left == 0 && right == 0)
        {
            randomMovement = Random.Range(1, 4);

            if (randomMovement == 1)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                PosY = PosY - 1;
                rb.linearVelocity = new Vector3(0, -0, -11);
            }
            if (randomMovement == 2)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX, PosY + 1, (GridValue));
                PosY = PosY + 1;
                rb.linearVelocity = new Vector3(0, 0, 11);
            }
            if (randomMovement == 3)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX - 1, PosY, (GridValue));
                PosX = PosX - 1;
                rb.linearVelocity = new Vector3(-11, 0, 0);
            }
            if (randomMovement == 4)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                PosX = PosX + 1;
                rb.linearVelocity = new Vector3(11, 0, 0);
            }
        }
        else
        {
            Debug.Log("Not Accounted for");
        }
        StartCoroutine(DelayCoroutine());
        DelayCoroutine();
    }
}