using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class ghost : Common
{
    
    private int down, up, left, right;
    private bool downBlocked, upBlocked, leftBlocked, rightBlocked;
    private Rigidbody rb;
    int randomMovement;
    public int ghostSpeed = 10;
    bool playerC = false;
    protected override void Start()
    {
       base.Start();
        rb = GetComponent<Rigidbody>();

        // Set default values to false.
        clearBlocks();

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
        // Grab whether a direction is blocked or not.
        down = gt.grid.GetValue(PosX, PosY - 1);
        up = gt.grid.GetValue(PosX, PosY + 1);
        left = gt.grid.GetValue(PosX - 1, PosY);
        right = gt.grid.GetValue(PosX + 1, PosY);

        // Check for blockages at the start.
        if (down == 1 || down == 2 || down == -1)
        {
            downBlocked = true;
        }
        if (left == -1 || left == 1 || left == 2)
        {
            leftBlocked = true;
        }
        if (right == -1 || right == 1 || right == 2)
        {
            rightBlocked = true;
        }
        if (up == -1 || up == 1 || up == 2)
        {
            upBlocked = true;
        }

        // Checking for player in adjacent space.
        if ((down == 3) || (up == 3) || (left == 3) || (right == 3))
        {
            if (down == 3)
            {
                moveDown();
            }
            if (up == 3)
            {
                moveUp();
            }
            if (left == 3)
            {
                moveLeft();

            }
            if (right == 3)
            {
                moveRight();
            }
        }
        // The blockages checks.
        else if (upBlocked || downBlocked || leftBlocked || rightBlocked)
        {
            // If blocked on all sides, give debug message.
            if (upBlocked && downBlocked && leftBlocked && rightBlocked)
            {
                Debug.Log("Ghost Trapped");
            }
            // If blocked on up/down/left, move right.
            else if (downBlocked && upBlocked && leftBlocked)
            {
                moveRight();
            }
            // If blocked on down/up/right, move left.
            else if (downBlocked && upBlocked && rightBlocked)
            {
                moveLeft();

            }
            // If blocked down/right/left, move up.
            else if (downBlocked && rightBlocked && leftBlocked)
            {
                moveUp();
            }
            // If blocked right up and left, move down.
            else if (downBlocked && rightBlocked && leftBlocked)
            {
                moveDown();
            }
            // If blocked down/up, move left or right randomly.
            else if (downBlocked && upBlocked)
            {
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    moveRight();

                }
                if (randomMovement == 2)
                {
                    moveLeft();
                }

            }
            // If blocked down/left, move right or up randomly.
            else if (downBlocked && leftBlocked)
            {
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    moveUp();
                }
                if (randomMovement == 2)
                {
                    moveRight();
                }
            }
            // If down/right blocked, move up or left randomly.
            else if (downBlocked && rightBlocked)
            {
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    moveLeft();
                }
                if (randomMovement == 2)
                {
                    moveUp();

                }
            }
            // If up/left blocked, move down or right randomly.
            else if (upBlocked && leftBlocked)
            {
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    moveRight();
                }
                if (randomMovement == 2)
                {
                    moveDown();
                }
            }
            // if up/right blocked, move down or left randomly.
            else if (upBlocked && rightBlocked)
            {
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    moveLeft();
                }
                if (randomMovement == 2)
                {
                    moveDown();

                }
            }
            // If left/right blocked, move up or down randomly.
            else if (leftBlocked && rightBlocked)
            {
                randomMovement = Random.Range(1, 3);
                if (randomMovement == 1)
                {
                    moveDown();
                }
                if (randomMovement == 2)
                {
                    moveUp();
                }
            }
            // if down blocked, move elsewhere randomly.
            else if (downBlocked)
            {
                randomMovement = Random.Range(1, 4);
                if (randomMovement == 1)
                {
                    moveRight();
                }
                if (randomMovement == 2)
                {
                    moveLeft();

                }
                if (randomMovement == 3)
                {
                    moveUp();
                }
            }
            // If up blocked, move elsewhere randomly.
            else if (upBlocked)
            {
                randomMovement = Random.Range(1, 4);
                if (randomMovement == 1)
                {
                    moveRight();
                }
                if (randomMovement == 2)
                {
                    moveLeft();

                }
                if (randomMovement == 3)
                {
                    moveDown();
                }
            }
            // If left blocked, move elsewhere randomly.
            else if (leftBlocked)
            {
                Debug.Log("left wall");
                randomMovement = Random.Range(1, 4);
                if (randomMovement == 1)
                {
                    moveDown();
                }
                if (randomMovement == 2)
                {
                    moveUp();
                }
                if (randomMovement == 3)
                {
                    moveRight();
                }
            }
            // If right blocked, move elsewhere randomly.
            else if (rightBlocked)
            {
                Debug.Log("right wall");
                randomMovement = Random.Range(1, 4);
                if (randomMovement == 1)
                {
                    moveUp();
                }
                if (randomMovement == 2)
                {
                    moveLeft();

                }
                if (randomMovement == 3)
                {
                    moveDown();
                }
            }

        }
        // If unblocked, move anywhere randomly.
        else if (down == 0 && up == 0 && left == 0 && right == 0)
        {
            randomMovement = Random.Range(1, 5);

            if (randomMovement == 1)
            {
                moveDown();
            }
            if (randomMovement == 2)
            {
                moveUp();
            }
            if (randomMovement == 3)
            {
                moveLeft();
            }
            if (randomMovement == 4)
            {
                moveRight();
            }
        }
        StartCoroutine(DelayCoroutine());
        DelayCoroutine();
    }

    private void moveLeft()
    {
        gt.grid.SetValue(PosX, PosY, (0));
        gt.grid.SetValue(PosX - 1, PosY, (GridValue));
        PosX = PosX - 1;
        rb.linearVelocity = new Vector3(-11, 0, 0);
        Debug.Log("Moved left!");
        clearBlocks();
    }
    private void moveRight()
    {
        gt.grid.SetValue(PosX, PosY, (0));
        gt.grid.SetValue(PosX + 1, PosY, (GridValue));
        PosX = PosX + 1;
        rb.linearVelocity = new Vector3(11, 0, 0);
        Debug.Log("Moved right!");
        clearBlocks();
    }

    private void moveUp()
    {
        gt.grid.SetValue(PosX, PosY, (0));
        gt.grid.SetValue(PosX, PosY + 1, (GridValue));
        PosY = PosY + 1;
        rb.linearVelocity = new Vector3(0, 0, 11);
        Debug.Log("Moved up!");
        clearBlocks();
    }

    private void moveDown()
    {
        gt.grid.SetValue(PosX, PosY, (0));
        gt.grid.SetValue(PosX, PosY - 1, (GridValue));
        PosY = PosY - 1;
        rb.linearVelocity = new Vector3(0, 0, -11);
        Debug.Log("Moved down!");
        clearBlocks();
    }

    private void clearBlocks()
    {
        downBlocked = false;
        leftBlocked = false;
        downBlocked = false;
        rightBlocked = false;
    }
}