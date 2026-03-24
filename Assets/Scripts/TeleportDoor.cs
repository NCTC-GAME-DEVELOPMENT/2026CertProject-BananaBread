using UnityEngine;

public class TeleportDoor : MonoBehaviour
{

    // Teleport destination is an empty on the object.
    public GameObject emptyLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            collision.gameObject.transform.position = emptyLocation.transform.position;
        }
    }
}
