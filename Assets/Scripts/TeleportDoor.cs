using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Build.Content;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{

    // Choose grid location for exit.
    public int gridX, gridY;
    public bool DestinationNorthWall = false;

    //Game Manager variable.
    private GameObject manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Grab game manager at start.
        manager = GameObject.Find("GameManager");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if collission was player controller.
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        // If player controller, teleport to empty.
        if (player)
        {

            // Get the grid.
            Grid_testing grid = manager.GetComponent<Grid_testing>();
            // Find destination.
            Vector3 destination = grid.grid.GetWorldPosition(gridX, gridY);
            // Get destination cell's value.
            int destinationSpace = grid.grid.GetValue(gridX, gridY);
            // Correct the Y so that it isn't embedded in the floor.
            destination.y = 1f;

            // Add half the cell size to center it on the grid cell.
            destination.x = destination.x + (grid.cellSize/2f);
            if (DestinationNorthWall) destination.z = destination.z - (grid.cellSize / 2f);
            else destination.z = destination.z + (grid.cellSize / 2f);

            // If the grid is a 0 value...
            if (destinationSpace == 0)
            {
                // Teleport.
                collision.gameObject.transform.position = destination;
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
 * Vector3 location = gameObject.transform.position = gt.grid.GetWorldPosition(PosX, PosY);
 * location.x = location.x + (grid.cellSize/2f);
 * location.z = location.z + (grid.cellSize/2f);
 * gameObject.transform.position = location;
 * I feel this and similar cellsize adjustments should be used to teleport to set location in the grid.
 */