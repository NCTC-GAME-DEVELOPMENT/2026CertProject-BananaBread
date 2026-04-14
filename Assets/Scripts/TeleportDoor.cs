using System.Globalization;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/*
 * Not one behavior.
 * You can't push a crate into a door.
 * 
 * This is because of how crates innately work, and the CanPushHere function.
 * 
 * Would need to allow crates to sense doors somehow, and then add that to the push conditions that bypasses CanPushHere.
 * 
 * Short-term hacky answer is to design puzzles so that crates can only be pushed into doors, and out from doors. Never pushed in while at door position.
 */


public class TeleportDoor : Common
{

    public TeleportDoor destinationDoor;
    bool crateTeleported = false;

    //Grid_testing variable.
    private Grid_testing grid;

    private Crate[] crates;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        // Get the grid.
        grid = GameObject.Find("GameManager").GetComponent<Grid_testing>();



        Vector3 location = gameObject.transform.position = grid.grid.GetWorldPosition(PosX, PosY);
        if(Facing == currentDirection.West)
        {
            location.z = location.z + (grid.cellSize / 2f);
            location.x = location.x + (grid.cellSize);
        }
        else if (Facing == currentDirection.East)
        {
            location.z = location.z + (grid.cellSize / 2f);
        }
        else if (Facing == currentDirection.North)
        {
            //location.z = location.z + (grid.cellSize /2f);
            location.x = location.x + (grid.cellSize /2f);
        }
        else
        {
            location.x = location.x + (grid.cellSize / 2f);
            location.z = location.z + (grid.cellSize);

        }

        location.y = 1;
        gameObject.transform.position = location;

        // Create an array containing all crates.
        crates = Object.FindObjectsByType<Crate>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }

    // Update for moving crates.
    protected override void Update()
    {
        // If a crate hasn't been teleported.
        if (!crateTeleported)
        {
            // Loop through the crates.
            for (int x = 0; x < crates.Length; x++)
            {
                // If it's at the current teleporter...
                if (crates[x] && crates[x].PosX == PosX && crates[x].PosY == PosY)
                {
                    //.. mark the destination as being teleported.
                    destinationDoor.crateTeleported = true;

                    // Find destination.
                    Vector3 destination = grid.grid.GetWorldPosition(destinationDoor.PosX, destinationDoor.PosY);
                    // Get destination cell's value.
                    int destinationSpace = grid.grid.GetValue(destinationDoor.PosX, destinationDoor.PosY);
                    // Correct the Y so that it isn't embedded in the floor.
                    destination.y = 1f;

                    // Add half the cell size to center it on the grid cell.
                    destination.x = destination.x + (grid.cellSize / 2f);
                    destination.z = destination.z + (grid.cellSize / 2f);

                    //Set the old position to 0.
                    gt.grid.SetValue(PosX, PosY, (0));
                    // Set the PosX and PosY to the new destination.
                    // Set its internal position to the position.
                    crates[x].ChangeLocation(destinationDoor.PosX, destinationDoor.PosY, destination);
                    // Set the destination to the new value..
                    gt.grid.SetValue(destinationDoor.PosX, destinationDoor.PosY, (2));

                }
            }
        }
        // Remove the condition after the teleporter has been cleared again.
        else if (grid.grid.GetValue(PosX, PosY) != 2)
        {
            crateTeleported = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if collission was player controller.
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        // If player controller, teleport to empty.
        if (player)
        {
            // Find destination.
            Vector3 destination = grid.grid.GetWorldPosition(destinationDoor.PosX, destinationDoor.PosY);
            // Get destination cell's value.
            int destinationSpace = grid.grid.GetValue(destinationDoor.PosX, destinationDoor.PosY);
            // Correct the Y so that it isn't embedded in the floor.
            destination.y = 1f;

            // Add half the cell size to center it on the grid cell.
            destination.x = destination.x + (grid.cellSize / 2f);
            destination.z = destination.z + (grid.cellSize / 2f);

            // If the grid is a 0 value...
            if (destinationSpace == 0)
            {
                // Teleport.
                player.PosX = destinationDoor.PosX;
                player.PosY = destinationDoor.PosY;
                gt.grid.SetValue(PosX, PosY, (0));
                collision.gameObject.transform.position = destination;
            } // Else if there's a pushable block.
            else if (destinationSpace == 2)
            {
                // First, convert the facing direction to string.
                string doorFacing = destinationDoor.Facing.ToString();

                for (int x = 0; x < crates.Length; x++)
                {
                    // Find the crate at the destination door.
                    if (crates[x].PosX == destinationDoor.PosX && crates[x].PosY == destinationDoor.PosY)
                    {
                        //Try to push it.
                        crates[x].MoveCrate(doorFacing);

                        // Re-obtain the value.
                        destinationSpace = grid.grid.GetValue(destinationDoor.PosX, destinationDoor.PosY);

                        // If it's empty now..
                        if (destinationSpace == 0)
                        {
                            // Teleport.
                            player.PosX = destinationDoor.PosX;
                            player.PosY = destinationDoor.PosY;
                            gt.grid.SetValue(PosX, PosY, (0));
                            player.gameObject.transform.position = destination;
                        }
                    }
                }

            }
        }

    }
    
}
/*
 * Pseudo code idea: how to handle crates going through the teleport door.
 * 
 * First: the collission box only checks if it enters collision, not teleports. It has a bool for "inFrontOfDoor".
 * 
 * Second: only teleports if pushed into the door.
 * 
 * Third: If a player enters, and it's blocked by CRATE, attempt to push that crate outward.
 * May need to add the north/south/east/west enum here to tell it which way to attempt to shove the crate in the destination.
 * If it works, teleport player after the push.
 * 
 * 
 * Also, these lines:         
        Vector3 location = gameObject.transform.position = gt.grid.GetWorldPosition(PosX, PosY);
        location.x = location.x + (gt.cellSize / 2f);
        location.z = location.z + (gt.cellSize / 2f);
        gameObject.transform.position = location;
 */