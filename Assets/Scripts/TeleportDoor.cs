using UnityEditor.Build.Content;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{

    // Choose grid location for exit.
    public int gridX, gridY;

    //Game Manager grabbed.
    private GameObject manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Grab game manager at start.
        manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
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
            // Teleport.
            collision.gameObject.transform.position = destination;
        }
    }
}
