using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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


        // Turn it into a list.
        var directionList = new List<Action>();
        directionList.Add(moveUp);
        directionList.Add(moveDown);
        directionList.Add(moveLeft);
        directionList.Add(moveRight);

        // Check for blockages at the start.
        // .. and remove from list invalid directions.
        if (down == 1 || down == 2 || down == -1)
        {
            downBlocked = true;
            directionList.Remove(moveUp);
        }
        if (left == -1 || left == 1 || left == 2)
        {
            leftBlocked = true;
            directionList.Remove(moveLeft);
        }
        if (right == -1 || right == 1 || right == 2)
        {
            rightBlocked = true;
            directionList.Remove(moveRight);
        }
        if (up == -1 || up == 1 || up == 2)
        {
            upBlocked = true;
            directionList.Remove(moveDown);
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
        // If there are no movement options, debug message.
        else if (directionList.Count() == 0)
        {
            Debug.Log("Ghost Trapped");
            // End function.
            return;
        }
        // Otherwise..
        else
        {
            // Grab a direction index from the list.
            int randomIndex = UnityEngine.Random.Range(0, directionList.Count);
            // Run it.
            directionList[randomIndex].Invoke();
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