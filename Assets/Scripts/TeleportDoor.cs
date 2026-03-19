using UnityEngine;

public class TeleportDoor : MonoBehaviour
{

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
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            collision.gameObject.transform.position = emptyLocation.transform.position;
        }
    }
}
