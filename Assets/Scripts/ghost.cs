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
    private currentDirection doorFacing = currentDirection.None;

    private Vector3 doorDestination;
    private int doorX, doorY;

    private TeleportDoor[] doors;
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
        players = Object.FindObjectsByType<PlayerController>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        // Grab all doors.
        doors = Object.FindObjectsByType<TeleportDoor>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
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

        // Set the facing direction for the door if on one.
        SetDoorFacing();

        // Remove invalid directions from list, only if there's no teleport door there.
        if ((down == 1 || down == 2 || down == -1) && !(doorFacing == currentDirection.South))
        {
            directionList.Remove("Down");
        }
        if ((left == -1 || left == 1 || left == 2) && !(doorFacing == currentDirection.West))
        {
            directionList.Remove("Left");
        }
        if ((right == -1 || right == 1 || right == 2) && !(doorFacing == currentDirection.East))
        {
            directionList.Remove("Right");
        }
        if ((up == -1 || up == 1 || up == 2) && !(doorFacing == currentDirection.North))
        {
            directionList.Remove("Up");
        }

        // Check only available directions for player.
        for(int x = 0; x < directionList.Count; x++)
        {
            FoundPlayer(directionList[x], PosX, PosY);
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




            if (doorFacing == currentDirection.North && directionList[randomIndex] == "Up")
            {
                ChangeLocation(doorX, doorY, doorDestination);
            }
            else if (doorFacing == currentDirection.South && directionList[randomIndex] == "Down")
            {
                ChangeLocation(doorX, doorY, doorDestination);
            }
            else if (doorFacing == currentDirection.East && directionList[randomIndex] == "Right")
            {
                ChangeLocation(doorX, doorY, doorDestination);
            }
            else if (doorFacing == currentDirection.West && directionList[randomIndex] == "Left")
            {
                ChangeLocation(doorX, doorY, doorDestination);
            }
            else
            {
                // Else just move.
                moveGhost(directionList[randomIndex]);
            }
        }

        // If it reached this point it moved normally.
        // So delay by speed.
        StartCoroutine(DelayCoroutine(ghostSpeed));
        DelayCoroutine(ghostSpeed);
    }

    private void FoundPlayer(string direction, int XPos, int YPos)
    {
        int gridValue;

        if (direction == "Down")
        {
            // Get value of the requested grid.
            gridValue = gt.grid.GetValue(XPos, YPos - 1);
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
            // Get value of the requested grid.
            gridValue = gt.grid.GetValue(XPos, YPos + 1);
            // Return true if player found.
            if (gridValue == 3)
            {
                playerUp = true;
            }
            // Move further into the grid if empty space.
            else if (gridValue == 0)
            {
                YPos += 1;
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
            // Get value of the requested grid.
            gridValue = gt.grid.GetValue(XPos - 1, YPos);
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
            // Get value of the requested grid.
            gridValue = gt.grid.GetValue(XPos + 1, YPos);
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

    private void SetDoorFacing()
    {




        // Loop through the doors.
        for (int x = 0; x < doors.Length; x++)
        {
            // If it's at the current teleporter...
            if (doors[x] && doors[x].PosX == PosX && doors[x].PosY == PosY)
            {
                // Get the facing direction.
                doorFacing = doors[x].Facing;

                // And get its destination.
                // Find destination.
                doorDestination = gt.grid.GetWorldPosition(doors[x].destinationDoor.PosX, doors[x].destinationDoor.PosY);
                // Get destination cell's value.
                int destinationSpace = gt.grid.GetValue(doors[x].destinationDoor.PosX, doors[x].destinationDoor.PosY);

                // Add half the cell size to center it on the grid cell.
                doorDestination.x = doorDestination.x + (gt.cellSize / 2f);
                doorDestination.z = doorDestination.z + (gt.cellSize / 2f);

                //set X and Y.
                doorX = doors[x].destinationDoor.PosX;
                doorY = doors[x].destinationDoor.PosY;
                // return.
                return;
            }
            else
            {
                doorFacing = currentDirection.None;
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