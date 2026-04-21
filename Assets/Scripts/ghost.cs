using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : Common
{

    private int down, up, left, right;
    public int ghostSpeed = 1;
    public int resetTime = 5;
    bool playerC = false;

    private bool playerDown, playerUp, playerLeft, playerRight;

    private PlayerController[] players;
    protected override void Start()
    {
        base.Start();

        GridValue = 4;
        // Grab starting countdown time. + 1 for "Go" still gripping player,
        int startCountdown = gm.GetComponent<Countdown>().TimeValue + 1;
        // Delay at start by countdown timer.
        StartCoroutine(DelayCoroutine(startCountdown));
        gt.grid.SetValue(PosX, PosY, (GridValue));
        DelayCoroutine(startCountdown);
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
        // Grab the value of the adjacent squares.
        down = gt.grid.GetValue(PosX, PosY - 1);
        up = gt.grid.GetValue(PosX, PosY + 1);
        left = gt.grid.GetValue(PosX - 1, PosY);
        right = gt.grid.GetValue(PosX + 1, PosY);

        // Turn move options into a list.
        List<string> directionList = new List<string>();
        directionList.Add("Up");
        directionList.Add("Down");
        directionList.Add("Left");
        directionList.Add("Right");

        // Remove invalid directions from list.
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

        // Set the bools for if a player is found.
        FoundPlayer("Down", PosX, PosY - 1);
        FoundPlayer("Up", PosX, PosY + 1);
        FoundPlayer("Left", PosX - 1, PosY);
        FoundPlayer("Right", PosX + 1, PosY);

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
            StartCoroutine(DelayCoroutine(resetTime));
            DelayCoroutine(resetTime);
            // Return so that it doesn't continue moving despite the delay timer.
            return;
        }

        if (playerC)
        {
            // Condition for skipping the rest of the else if statements if player captured.
        }
        // If player was found, move
        else if (playerDown)
        {
            moveGhost("Down");
        }
        else if (playerUp)
        {
            moveGhost("Up");
        }
        else if (playerLeft)
        {
            moveGhost("Left");
        }
        else if (playerRight)
        {
            moveGhost("Right");
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

    private void FoundPlayer(string direction, int XPos, int YPos)
    {
        // Get value of the requested grid.
        int gridValue = gt.grid.GetValue(XPos, YPos);

        if (direction == "Down")
        {
            // Return true if player found.
            if (gridValue == 3)
            {
                playerDown = true;
            }
            // Move further into the grid if empty space.
            else if (gridValue == 0)
            {
                YPos -= 1;
                FoundPlayer("Down", XPos, YPos);
            }
            // Return false if hit any non-open or non-player space.
            else
            { 
                playerDown = false;
            }

        }
        else if (direction == "Up")
        {
            // Return true if player found.
            if (gridValue == 3)
            {
                playerUp = true;
            }
            // Move further into the grid if empty space.
            else if (gridValue == 0)
            {
                YPos += 2;
                FoundPlayer("Up", XPos, YPos);
            }
            // Return false if hit any non-open or non-player space.
            else
            {
                playerUp = false;
            }

        }
        else if (direction == "Left")
        {
            // Return true if player found.
            if (gridValue == 3)
            {
                playerLeft = true;
            }
            // Move further into the grid if empty space.
            else if (gridValue == 0)
            {
                XPos -= 1;
                FoundPlayer("Left", XPos, YPos);
            }
            // Return false if hit any non-open or non-player space.
            else
            {
                playerLeft = false;
            }

        }
        else if (direction == "Right")
        {
            // Return true if player found.
            if (gridValue == 3)
            {
                playerRight = true;
            }
            // Move further into the grid if empty space.
            else if (gridValue == 0)
            {
                XPos += 1;
                FoundPlayer("Right", XPos, YPos);
            }
            // Return false if hit any non-open or non-player space.
            else
            {
                playerRight = false;
            }

        }
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