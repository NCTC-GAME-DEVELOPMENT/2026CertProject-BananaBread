using UnityEngine;

public class ExitDoor : MonoBehaviour
{

    // Bool to check if crate has been sent.
    bool CrateSent = false;
    // Bool check for first player going through.
    bool PlayerLeft = false;

    public int locationGridX, locationGridY;


    public enum currentDirection
    {
        North,
        East,
        South,
        West
    }

    public currentDirection onWallFacing;

    //Grid_testing variable.
    private Grid_testing grid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the grid.
        grid = GameObject.Find("GameManager").GetComponent<Grid_testing>();

        Vector3 location = gameObject.transform.position = grid.grid.GetWorldPosition(locationGridX, locationGridY);
        if (onWallFacing == currentDirection.West)
        {
            location.z = location.z + (grid.cellSize / 2f);
            location.x = location.x + (grid.cellSize);
        }
        else if (onWallFacing == currentDirection.East)
        {
            location.z = location.z + (grid.cellSize / 2f);
        }
        else
        {
            location.x = location.x + (grid.cellSize / 2f);

        }
        gameObject.transform.position = location;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Create variables for player and crate.
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        QueryCrate winCrate = collision.gameObject.GetComponent<QueryCrate>();

        // If it's the crate, declare it being sent as true and delete the crate.
        if(winCrate)
        {
            CrateSent = true;
            Destroy(collision.gameObject);
        }

        // If the player goes into the door,
        // and the other player hasn't left,
        // and the crate has been sent through,
        // delete player and mark a player as left.
        // Null the variable so that it doesn't then immediately run a win condition.
        if (player && !PlayerLeft && CrateSent)
        {
            Destroy(collision.gameObject);
            PlayerLeft = true;
            player = null;
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
