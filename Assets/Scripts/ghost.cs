using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : Common
{

    private int down, up, left, right;
    int randomMovement;
    public int ghostSpeed = 1;
    bool playerC = false;

    private PlayerController[] players;
    protected override void Start()
    {
        base.Start();


        GridValue = 4;
        //Delay at start until 3 second starting timer is up.
        StartCoroutine(DelayCoroutine(4));
        gt.grid.SetValue(PosX, PosY, (GridValue));
        DelayCoroutine(4);

        // Grab all players.
        players = UnityEngine.Object.FindObjectsByType<PlayerController>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

    }

    IEnumerator DelayCoroutine(int timer)
    {
        yield return new WaitForSeconds(timer);
        Move();
    }

    public void Move()
    {
        // Grab whether a direction is blocked or not.
        down = gt.grid.GetValue(PosX, PosY - 1);
        up = gt.grid.GetValue(PosX, PosY + 1);
        left = gt.grid.GetValue(PosX - 1, PosY);
        right = gt.grid.GetValue(PosX + 1, PosY);


        // Turn move functions into a list.
        List<string> directionList = new List<string>();
        directionList.Add("Up");
        directionList.Add("Down");
        directionList.Add("Left");
        directionList.Add("Right");

        // Remove from list invalid directions.
        if (down == 1 || down == 2 || down == -1)
        {
            directionList.Remove("Down");
        }
        if (left == -1 || left == 1 || left == 2)
        {
            directionList.Remove("Left");
        }
        if (right == -1 || right == 1 || right == 2)
        {
            directionList.Remove("Right");
        }
        if (up == -1 || up == 1 || up == 2)
        {
            directionList.Remove("Up");
        }

        // Run code to check if in the same position as a player.
        CapturePlayer();

        // If both players are in position...
        if ((players[0].PosX == PosX && players[0].PosY == PosY) && (players[1].PosX == PosX && players[1].PosY == PosY))
        {
            // Reset everything.
            ResetPosition();
            players[0].IsCaught = false;
            players[1].IsCaught = false;
            playerC = false;

            // Then time out briefly.
            StartCoroutine(DelayCoroutine(5));
            DelayCoroutine(5);
            // Return so that it doesn't continue moving despite the delay timer.
            return;

        }

        if (playerC)
        {
            // Condition for skipping the rest of the else if statements if player captured.
        }
        // Checking for player in adjacent space.
        else if ((down == 3) || (up == 3) || (left == 3) || (right == 3))
        {
            if (down == 3)
            {
                moveGhost("Down");
            }
            if (up == 3)
            {
                moveGhost("Up");
            }
            if (left == 3)
            {
                moveGhost("Left");
            }
            if (right == 3)
            {
                moveGhost("Right");
            }
        }
        // If there are no movement options, debug message.
        else if (directionList.Count == 0)
        {
            Debug.Log("Ghost Trapped");
            // End function.
            return;
        }
        // Otherwise..
        else
        {
            // Grab a direction index from the list randomly.
            int randomIndex = UnityEngine.Random.Range(0, directionList.Count);
            // Run it.
            moveGhost(directionList[randomIndex]);
        }

        // If it reached this point it moved normally.
        // So delay by speed.
        StartCoroutine(DelayCoroutine(ghostSpeed));
        DelayCoroutine(ghostSpeed);
    }

    // Code for setting bools to true for capture.
    private void CapturePlayer()
    {
        if (players[0].PosX == PosX && players[0].PosY == PosY)
        {
            playerC = true;
            players[0].IsCaught = true;
        }
        else if (players[1].PosX == PosX && players[1].PosY == PosY)
        {
            playerC = true;
            players[1].IsCaught = true;
        }
    }

    private void moveGhost(string direction)
    {
        // Set grid value to 0.
        gt.grid.SetValue(PosX, PosY, (0));
        // Get current location.
        Vector3 newLocation = myPosition;
        // Set new position.
        if (direction == "Right")
        {
            PosX += 1;
            newLocation.x += gt.cellSize;
        }
        else if (direction == "Left")
        {
            PosX -= 1;
            newLocation.x -= gt.cellSize;
        }
        else if (direction == "Up")
        {
            PosY += 1;
            newLocation.z += gt.cellSize;
        }
        else if (direction == "Down")
        {
            PosY -= 1;
            newLocation.z -= gt.cellSize;
        }

        // Correct position.
        myPosition = newLocation;
        // Set transform.
        gameObject.transform.position = newLocation;
        // Change grid value.
        gt.grid.SetValue(PosX, PosY, (GridValue));
        // Set myPosition.
        myPosition = gameObject.transform.position;
    }
}