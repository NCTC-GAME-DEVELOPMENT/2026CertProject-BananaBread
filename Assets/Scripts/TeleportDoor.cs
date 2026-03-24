using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Build.Content;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{

    // Choose grid location for exit.
    public int gridX, gridY;

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
            // Find destination.
            Vector3 destination = manager.GetComponent<Grid_testing>().grid.GetWorldPosition(gridX, gridY);
            // Get destination cell's value.
            int destinationSpace = manager.GetComponent<Grid_testing>().grid.GetValue(gridX, gridY);
            // Correct the Y so that it isn't embedded in the floor.
            destination.y = 1f;
            // If the grid is a 0 value...
            if (destinationSpace == 0)
            {
                // Teleport.
                collision.gameObject.transform.position = destination;
            }
        }
    }
}
