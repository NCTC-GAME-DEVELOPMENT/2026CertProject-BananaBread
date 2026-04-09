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
    bool playerC = false;
    protected override void Start()
    {
       base.Start();
        rb = GetComponent<Rigidbody>();

        GridValue = 4;
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
        if ((down == 3) || (up == 3) || (left == 3) || (right == 3))
        {
            if (down == 3)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                PosY = PosY - 1;
                rb.linearVelocity = new Vector3(0, -0, -11);
                playerC = true;
            }
            if (up == 3)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX, PosY + 1, (GridValue));
                PosY = PosY + 1;
                rb.linearVelocity = new Vector3(0, 0, 11);
                playerC = true;
            }
            if (left == 3)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX - 1, PosY, (GridValue));
                PosX = PosX - 1;
                rb.linearVelocity = new Vector3(-11, 0, 0);
                playerC = true;

            }
            if (right == 3)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                PosX = PosX + 1;
                rb.linearVelocity = new Vector3(11, 0, 0);
                playerC = true;
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
                rb.linearVelocity = new Vector3(0, 0, -11);
            }
            if ((down == 1) && (up == 1) || (down == 2) && (up == 1) || (down == 1) && (up == 2) || (down== 2) && (up == 2))
            {
                randomMovement = Random.Range(1, 3);
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
                    gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                    PosX = PosX + 1;
                    rb.linearVelocity = new Vector3(11, 0, 0);
                }
            }
            if ((down == 1) && (right == 1) || (down == 2) && (right == 1) || (down == 1) && (right == 2))
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
                    gt.grid.SetValue(PosX - 1, PosY, (GridValue));
                    PosX = PosX - 1;
                    rb.linearVelocity = new Vector3(-11, 0, 0);

                }
            }
            if ((up == 1) && (left == 1) || (up == 2) && (left == 1) || (up == 1) && (left == 2))
            {
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                    PosY = PosY - 1;
                    rb.linearVelocity = new Vector3(0, 0, -11);
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
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                    PosY = PosY - 1;
                    rb.linearVelocity = new Vector3(0, 0, -11);
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
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                    PosY = PosY - 1;
                    rb.linearVelocity = new Vector3(0, 0, -11);
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
                randomMovement = Random.Range(1, 4);
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
                    gt.grid.SetValue(PosX + 1, PosY, (GridValue));
                    PosX = PosX + 1;
                    rb.linearVelocity = new Vector3(11, 0, 0);
                }
            }
            if ((up == 1) || (up == 2))
            {
                randomMovement = Random.Range(1, 4);
                if (randomMovement == 1)
                {
                    gt.grid.SetValue(PosX, PosY, (0));
                    gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                    PosY = PosY - 1;
                    rb.linearVelocity = new Vector3(0, 0, -11);
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
                randomMovement = Random.Range(1, 4);
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
                    rb.linearVelocity = new Vector3(0, 0, -11);
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
                randomMovement = Random.Range(1, 4);
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
                    rb.linearVelocity = new Vector3(0, 0, -11);
                }
            }

        }
        if (down == 0 && up == 0 && left == 0 && right == 0)
        {
            randomMovement = Random.Range(1, 5);

            if (randomMovement == 1)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                gt.grid.SetValue(PosX, PosY - 1, (GridValue));
                PosY = PosY - 1;
                rb.linearVelocity = new Vector3(0, 0, -11);
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
        StartCoroutine(DelayCoroutine());
        DelayCoroutine();
    }
}