using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class ExitDoor : Common
{

    // Bool to check if crate has been sent.
    bool CrateSent = false;
    // Bool check for first player going through.
    bool PlayerLeft = false;

    float timer = 0f;
    // Set to animation time of QueryCrate.
    float waitTime = 0.5f;

    public string sceneName;

    //Grid_testing variable.
    private Grid_testing grid;

    //Get the game manager.
    private GameManager manager;

    //A list is a bit better to work with than an array.
    private List<QueryCrate> winCrates = new List<QueryCrate>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        // Start the timer.
        timer = waitTime;

        // Get the grid.
        grid = GameObject.Find("GameManager").GetComponent<Grid_testing>();

        // Get the manager.
        manager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();

        //Default scene name to reset the local scene.
        if (sceneName.Length == 0) sceneName = SceneManager.GetActiveScene().name;

        Vector3 location = gameObject.transform.position = grid.grid.GetWorldPosition(PosX, PosY);

        location.y = 2f;

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

        // Find the Query Crates for the scene.
        QueryCrate[] tempCrates = Object.FindObjectsByType<QueryCrate>(FindObjectsSortMode.None);
        // Add to the list for code use.
        winCrates.AddRange(tempCrates);
        // Log the find.
        Debug.Log($"Found Crates: {tempCrates.Length}!");

    }

    protected override void Update()
    {
        base.Update();

        // First if statement; if there are still winCrates in the list.
        if (winCrates.Count > 0)
        {
            // Loop through all available crates.
            for (int x = 0; x < winCrates.Count; x++)
            {
                // If one of those crates is in position, 
                if (winCrates[x].PosX == PosX && winCrates[x].PosY == PosY)
                {
                    // Start the timer.
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    // Once the timer is up..
                    else
                    {
                        SendQueryCrate(winCrates[x]);
                        timer = waitTime;
                    }


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

        // If there's a player, someone hasn't left yet, and the crates are gone...
        if (player && !PlayerLeft && CrateSent)
        {
            // Destroy 
            Destroy(collision.gameObject);
            // Changes grid value to 0.
            gt.grid.SetValue(PosX, PosY, (0));
            // Mark that a player has left.
            PlayerLeft = true;
            // Empty variable so that it doesn't just go "Player wins!" immediately.
            player = null;
        }

        // If the player enters after the create is sent, win.
        if (player && PlayerLeft && CrateSent)
        {
            Destroy(collision.gameObject);
            manager.ClearLevel();
        }
    }

    // Turned this into a script so that it can be called by the crate.
    public void SendQueryCrate(QueryCrate inCrate)
    {
        // Loop through the crates.
        for (int x = 0; x < winCrates.Count; x++)
        {
            // If one of those crates is the caller. 
            if (winCrates[x] == inCrate)
            {
                // Destroy the crate.
                Destroy(inCrate.gameObject);
                // Remove from the list.
                winCrates.RemoveAt(x);
                // Changes grid value to 0.
                gt.grid.SetValue(PosX, PosY, (0));
                // Log the change.
                Debug.Log($"Crates left: {winCrates.Count}!");
            }
        }
    }
}
