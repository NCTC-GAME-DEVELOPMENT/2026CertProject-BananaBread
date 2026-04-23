using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ghost : Common
{
    private int down, up, left, right;
    public float ghostSpeed = 1.5f;
    public float resetTime = 5.0f;
    bool playerC = false;

    public Animator anim;

    // Player checks.
    private bool playerDown, playerUp, playerLeft, playerRight;

    // Arrays for tracking doors and players.
    private TeleportDoor[] doors;
    private PlayerController[] players;

    // initialize door variables to invalid values.
    // Destinatino X and Y for GetWorldPosition.
    private int destinationX = -1;
    private int destinationY = -1;
    // Door facing for making it move into the door.
    private currentDirection moveDirection = currentDirection.None;
    private currentDirection destinationDirection = currentDirection.None;
    // And the variable for holding GetWorldPosition.
    private Vector3 doorDestination = Vector3.zero;

    protected override void Start()
    {
        base.Start();

        // Grab animator.
        anim = GetComponentInChildren<Animator>();
        GridValue = 4;
        // Grab starting countdown time. + 1 for "Go".
        int startCountdown = gm.GetComponent<Countdown>().TimeValue + 1;
        gt.grid.SetValue(PosX, PosY, (GridValue));
        // Grab all players.
        players = Object.FindObjectsByType<PlayerController>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        // Grab all doors.
        doors = Object.FindObjectsByType<TeleportDoor>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        // Delay at start by countdown timer.
        StartCoroutine(DelayCoroutine(resetTime));
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

        // Set the door information for the door if on one.
        SetDoorFacing();

        // Remove invalid directions from list, only if there's no teleport door there.
        if ((down == 1 || down == 2 || down == -1) && !(moveDirection == currentDirection.South))
        {
            directionList.Remove("Down");
        }
        if ((left == -1 || left == 1 || left == 2) && !(moveDirection == currentDirection.West))
        {
            directionList.Remove("Left");
        }
        if ((right == -1 || right == 1 || right == 2) && !(moveDirection == currentDirection.East))
        {
            directionList.Remove("Right");
        }
        if ((up == -1 || up == 1 || up == 2) && !(moveDirection == currentDirection.North))
        {
            directionList.Remove("Up");
        }

        // Check only available directions for player.
        // Can't see through teleporter.
        for(int x = 0; x < directionList.Count; x++)
        {
            FoundPlayer(directionList[x], PosX, PosY);
        }

        // Run code to check if in the same position as a player.
        CapturePlayer();

        // If both players are in position...
        if ((players[0].PosX == PosX && players[0].PosY == PosY) && 
            (players[1].PosX == PosX && players[1].PosY == PosY))
        {
            // Reset everything.
            ResetPosition();
            players[0].IsCaught = false;
            players[1].IsCaught = false;
            playerC = false;

            // Then time out for resetTime.
            StartCoroutine(DelayCoroutine(resetTime));
            // Return so that it doesn't run movement code anyway.
            return;
        }

        // Movement if blocks.
        if (playerC)
        {
            // Condition for skipping the rest of the else if statements if player captured.
        }
        // If player was found, move towards them.
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

            // Use the door if it chose to move into a door..
            if ((moveDirection == currentDirection.North && directionList[randomIndex] == "Up") ||
            (moveDirection == currentDirection.South && directionList[randomIndex] == "Down") ||
            (moveDirection == currentDirection.East && directionList[randomIndex] == "Right") ||
            (moveDirection == currentDirection.West && directionList[randomIndex] == "Left"))
            {
                // Move.
                ChangeLocation(destinationX, destinationY, doorDestination);
                // .. and for the animation, because it animates after it moves.
                moveDirection = destinationDirection;
            }
            // Otherwise just move.
            else
            {
                // Else just move.
                moveGhost(directionList[randomIndex]);
            }
        }
        // Run animation coroutine.
        StartCoroutine(AnimationCoroutine(ghostSpeed));
    }

    public void RunAnimation(currentDirection moveDirection)
    {
        if (moveDirection == currentDirection.North)
        {
            anim.SetBool("moveNorth", true);
        }
        else if (moveDirection == currentDirection.South)
        {
            anim.SetBool("moveSouth", true);
        }
        else if(moveDirection == currentDirection.West)
        {
            anim.SetBool("moveWest", true);
        }
        else 
        {
            anim.SetBool("moveEast", true);
        }
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
            gridValue = gt.grid.GetValue(XPos, YPos + 1);
            if (gridValue == 3)
            {
                playerUp = true;
            }
            else if (gridValue == 0)
            {
                YPos += 1;
                FoundPlayer("Up", XPos, YPos);
            }
            else
            {
                playerUp = false;
            }

        }
        else if (direction == "Left")
        {
            gridValue = gt.grid.GetValue(XPos - 1, YPos);
            if (gridValue == 3)
            {
                playerLeft = true;
            }
            else if (gridValue == 0)
            {
                XPos -= 1;
                FoundPlayer("Left", XPos, YPos);
            }
            else
            {
                playerLeft = false;
            }

        }
        else if (direction == "Right")
        {
            gridValue = gt.grid.GetValue(XPos + 1, YPos);
            if (gridValue == 3)
            {
                playerRight = true;
            }
            else if (gridValue == 0)
            {
                XPos += 1;
                FoundPlayer("Right", XPos, YPos);
            }
            else
            {
                playerRight = false;
            }
        }
    }

    private currentDirection ReverseFacing(currentDirection inFacing)
    {
        if (inFacing == currentDirection.North)
        {
            inFacing = currentDirection.South;
        }
        else if (inFacing == currentDirection.South)
        {
            inFacing = currentDirection.North;
        }
        else if (inFacing == currentDirection.East)
        {
            inFacing = currentDirection.West;
        }
        else if (inFacing == currentDirection.West)
        {
            inFacing = currentDirection.East;
        }

        return inFacing;
    }

    private void SetDoorFacing()
    {
        // Loop through the doors.
        for (int x = 0; x < doors.Length; x++)
        {
            // If it's at a teleporter...
            if (doors[x].PosX == PosX && doors[x].PosY == PosY)
            {
                // Set all those values.
                destinationX = doors[x].destinationDoor.PosX;
                destinationY = doors[x].destinationDoor.PosY;
                // Wanted to try to animate on both sides, but probably need a single teleport animation that takes input destination and moves it from door.
                destinationDirection = doors[x].destinationDoor.Facing;
                moveDirection = ReverseFacing(doors[x].Facing);
                doorDestination = gt.grid.GetWorldPosition(destinationX, destinationY);
                // Add offsest so it isn't embedded in the floor or on a corner.
                // This is because of how GetWorldPosition works..
                doorDestination.x += gt.cellSize / 2;
                doorDestination.z += gt.cellSize / 2;
                doorDestination.y += 1.25f;
                return;
            }
            // Else if no door, zero the variables again.
            else if (moveDirection != currentDirection.None)
            {
                // initialize door variables to invalid values.
                // Destinatino X and Y for GetWorldPosition.
                destinationX = -1;
                destinationY = -1;
                // Door facing for making it move into the door.
                moveDirection = currentDirection.None;
                destinationDirection = currentDirection.None;
                // And the variable for holding GetWorldPosition.
                doorDestination = Vector3.zero;
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
        if (direction == "Left")
        {
            // Over-ride the move direction from the door value to the cell direction.
            moveDirection = currentDirection.West;
            PosX -= 1;
            newLocation.x -= gt.cellSize;
        }
        else if (direction == "Right")
        {
            moveDirection = currentDirection.East;
            PosX += 1;
            newLocation.x += gt.cellSize;
        }
        else if (direction == "Up")
        {
            moveDirection = currentDirection.North;
            PosY += 1;
            newLocation.z += gt.cellSize;
        }
        else if (direction == "Down")
        {
            moveDirection = currentDirection.South;
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

    // Animation timer.
    public void AnimationReset()
    {
        anim.SetBool("moveNorth", false);
        anim.SetBool("moveEast", false);
        anim.SetBool("moveSouth", false);
        anim.SetBool("moveWest", false);
    }

    // Delay timer.
    IEnumerator AnimationCoroutine(float time)
    {
        RunAnimation(moveDirection);
        // Time the wait for the animation speed.
        yield return new WaitForSeconds(0.5f);
        // Reset animations.
        AnimationReset();
        // Wait the rest of the move speed.
        yield return new WaitForSeconds(ghostSpeed - 0.5f); AnimationReset();
        // Return to Move,
        Move();
    }

    // Delay timer.
    IEnumerator DelayCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        Move();
    }
}