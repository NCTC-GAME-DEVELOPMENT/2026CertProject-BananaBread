using UnityEngine;

public class ExitDoor : MonoBehaviour
{

    // Bool to check if crate has been sent.
    public bool CrateSent = false;
    // Bool check for first player going through.
    public bool PlayerLeft = false;


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
        // Create variables for player and crate.
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        PlaceholderWinCondition winCrate = collision.gameObject.GetComponent<PlaceholderWinCondition>();

        // If it's the crate, declare it being sent as true and delete the crate.
        if(winCrate)
        {
            CrateSent = true;
            Destroy(collision.gameObject);
        }

        // If the player goes into the door,
        // and the player hasn't left,
        // and the crate has been sent through,
        // delete player and mark the first player as having left.
        if (player && !PlayerLeft && CrateSent)
        {
            Destroy(collision.gameObject);
            PlayerLeft = true;
        }

        // If the player enters after the create is sent, win.
        if (player && PlayerLeft && CrateSent)
        {
            PlayerWins();
        }
    }

    // Made a separate function for simpler modifying and use.
    public void PlayerWins()
    {
        Debug.Log("You win!");
    }
}
