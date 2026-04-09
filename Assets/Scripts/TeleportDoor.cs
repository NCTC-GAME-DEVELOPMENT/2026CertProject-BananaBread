using System.Globalization;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Build.Content;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TeleportDoor : Common
{

    public TeleportDoor destinationDoor;
    bool crateDetected = false;

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
        // The intention is that it'll later sense when a crate is on the teleport spot, and teleport it...
        // ... when it is shoved into the opposite direction of facing.
        // May require the function to be written on the crate side, though.
        crates = Object.FindObjectsByType<Crate>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
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
                collision.gameObject.transform.position = destination;
            }
            else if (destinationSpace == 2)
            {
                // First, convert the facing direction to string because Crate doesn't use the enum.
                string doorFacing;
                if (destinationDoor.Facing == currentDirection.North)
                {
                    doorFacing = "North";
                }
                else if (destinationDoor.Facing == currentDirection.South)
                {
                    doorFacing = "South";
                }
                else if (destinationDoor.Facing == currentDirection.West)
                {
                    doorFacing = "West";
                }
                else
                {
                    doorFacing = "East";
                }

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
                            collision.gameObject.transform.position = destination;
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