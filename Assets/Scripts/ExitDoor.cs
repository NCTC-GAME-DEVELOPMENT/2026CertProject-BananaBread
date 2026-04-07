using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : Common
{

    // Bool to check if crate has been sent.
    bool CrateSent = false;
    // Bool check for first player going through.
    bool PlayerLeft = false;

    public string sceneName;

    //Grid_testing variable.
    private Grid_testing grid;
    private QueryCrate[] winCrates;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        // Get the grid.
        grid = GameObject.Find("GameManager").GetComponent<Grid_testing>();

        //Default scene name to reset the local scene.
        if (sceneName.Length == 0) sceneName = SceneManager.GetActiveScene().name;

        Vector3 location = gameObject.transform.position = grid.grid.GetWorldPosition(PosX, PosY);

        location.y = 1;

        if (Facing == currentDirection.West)
        {
            location.z = location.z + (grid.cellSize / 2f);
            location.x = location.x + (grid.cellSize);
        }
        else if (Facing == currentDirection.East)
        {
            location.z = location.z + (grid.cellSize / 2f);
        }
        else
        {
            location.x = location.x + (grid.cellSize / 2f);

        }
        gameObject.transform.position = location;

        // Find the Query Crate for the scene.
        winCrates = Object.FindObjectsByType<QueryCrate>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }

    protected override void Update()
    {
        base.Update();

        // First if statement; if there are still winCrates in the array.
        if (winCrates.Length > 0)
        {
            // Loop through all available crates.
            for (int x = 0; x < winCrates.Length; x++)
            {
                // If one of those crates is in position, 
                if (winCrates[x].PosX == PosX && winCrates[x].PosY == PosY)
                {
                    Debug.Log("Win crate found in Exit Door!");
                    Destroy(winCrates[x].gameObject);
                }
            }
        }
        else
        {
            // Only when winCrates is empty, mark crates as sent.
            CrateSent = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Create variables for player and crate.
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

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
        // Currently rough.
        // If loading current scene, wont re-initialize the code.
        // Other scenes require being in the scene manager.
        SceneManager.LoadScene(sceneName);
    }
}
