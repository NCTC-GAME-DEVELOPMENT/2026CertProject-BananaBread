using System.Collections;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class Crate : Common
{
    float moveAmount = 3.0f;
    public Animator anim;

    // Array for all teleport doors in level.
    private TeleportDoor[] doors;

    //PlayerController senses a crate to push in front of it.
    //If success, pushes the crate in its facing direction, moving x or y based on the current push direction.

    protected override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();

        GridValue = 2;
        gt.grid.SetValue(PosX, PosY, (GridValue));

        // Get the doors.
        doors = Object.FindObjectsByType<TeleportDoor>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

    }
    public void MoveCrate(string direction)
    {
        Debug.Log("Push Detected");

        // Loop through the doors.
        for (int x = 0; x < doors.Length; x++)
        {
            // If it's at the current teleporter...
            if (doors[x] && doors[x].PosX == PosX && doors[x].PosY == PosY)
            {
                if ((direction == "North" && doors[x].Facing == currentDirection.South) || 
                    (direction == "South" && doors[x].Facing == currentDirection.North) ||
                    (direction == "East" && doors[x].Facing == currentDirection.West) ||
                    (direction == "West" && doors[x].Facing == currentDirection.East))
                {
                    // move it, if being pushed away from where the door is facing, that is, into the teleport.
                    doors[x].moveCrate(this);
                }

                // end function so that it doesn't then try to move again.
                return;
            }
        }
        if (CanPushHere(direction))
        {

            if (direction == "North")
            {
                anim.SetBool("PushNorth", true);
                gt.grid.SetValue(PosX, PosY, (0));
                PosY += 1;
                gt.grid.SetValue(PosX, PosY, (GridValue));
                gameObject.transform.position = new Vector3(myPosition.x, myPosition.y, (myPosition.z + moveAmount));
                myPosition = gameObject.transform.position;
                StartCoroutine(Cooldown(0.5f));
            }
            else if (direction == "South")
            {
                anim.SetBool("PushSouth", true);
                gt.grid.SetValue(PosX, PosY, (0));
                PosY -= 1;
                gt.grid.SetValue(PosX, PosY, (GridValue));
                gameObject.transform.position = new Vector3(myPosition.x, myPosition.y, (myPosition.z - moveAmount));
                myPosition = gameObject.transform.position;
                StartCoroutine(Cooldown(0.5f));
            }
            else if (direction == "East")
            {
                anim.SetBool("PushEast", true);
                gt.grid.SetValue(PosX, PosY, (0));
                PosX += 1;
                gt.grid.SetValue(PosX, PosY, (GridValue));
                gameObject.transform.position = new Vector3((myPosition.x + moveAmount), myPosition.y, myPosition.z);
                myPosition = gameObject.transform.position;
                StartCoroutine(Cooldown(0.5f));
            }
            else
            {
                anim.SetBool("PushWest", true);
                gt.grid.SetValue(PosX, PosY, (0));
                PosX -= 1;
                gt.grid.SetValue(PosX, PosY, (GridValue));
                gameObject.transform.position = new Vector3((myPosition.x - moveAmount), myPosition.y, myPosition.z);
                myPosition = gameObject.transform.position;
                StartCoroutine(Cooldown(0.5f));
            }
        }
    }

    protected bool CanPushHere(string direction)
    {
        //Tests the direction the grid is pushed in, if the spot in front of the crate is open, and if the spot in front of the crate is within the grid bounds.
        if (direction == "North" && gt.grid.GetValue(PosX, (PosY + 1)) == 0 && (PosY + 1) <= (gt.height - 1))
        {
            return true;
        }
        if (direction == "South" && gt.grid.GetValue(PosX, (PosY - 1)) == 0 && (PosY - 1) >= 0)
        {
            return true;
        }
        if (direction == "East" && gt.grid.GetValue((PosX + 1), PosY) == 0 && (PosX + 1) <= (gt.width - 1))
        {
            return true;
        }
        if (direction == "West" && gt.grid.GetValue((PosX - 1), PosY) == 0 && (PosX - 1) >= 0)
        {
            return true;
        }
        else
        {
            Debug.Log("You cannot push a crate here!");
            return false;
        }
    }

        IEnumerator Cooldown(float value)
        {
            yield return new WaitForSeconds(value);
            anim.SetBool("PushNorth", false);
            anim.SetBool("PushEast", false);
            anim.SetBool("PushSouth", false);
            anim.SetBool("PushWest", false);
        }
    }
