using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Plays sound effect one when sending querycrate.
// Plays sound effect two when sending player.

public class ExitDoor : Common
{

    // Bool to check if crate has been sent.
    bool CrateSent = false;
    // Bool check for first player going through.
    bool PlayerLeft = false;

    PlayerController P1;
    PlayerController P2;

    float timer = 0f;
    // Set to animation time of QueryCrate.
    float waitTime = 0.5f;

    public string sceneName;

    //A list is a bit better to work with than an array.
    private List<QueryCrate> winCrates = new List<QueryCrate>();
    private List<QueryCrate> recordedCrates = new List<QueryCrate>();

    //A variable used to keep track of the players left. Used for the win condition
    public int PlayersRemaining = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        P1 = GameObject.Find("P1").GetComponent<PlayerController>();
        P2 = GameObject.Find("P2").GetComponent<PlayerController>();

        // Start the timer.
        timer = waitTime;

        // Get the grid.
        gt = GameObject.Find("GameManager").GetComponent<Grid_testing>();

        // Get the manager.
        gm = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();

        //Default scene name to reset the local scene.
        if (sceneName.Length == 0) sceneName = SceneManager.GetActiveScene().name;

        Vector3 location = gameObject.transform.position = gt.grid.GetWorldPosition(PosX, PosY);

        location.y = 0;

        if (Facing == currentDirection.West)
        {
            location.z = location.z + (gt.cellSize / 2f);
            location.x = location.x + (gt.cellSize);
        }
        else if (Facing == currentDirection.East)
        {
            location.z = location.z + (gt.cellSize / 2f);
        }
        else
        {
            location.x = location.x + (gt.cellSize / 2f);

        }
        gameObject.transform.position = location;

        // Find the Query Crates for the scene.
        QueryCrate[] tempCrates = Object.FindObjectsByType<QueryCrate>(FindObjectsSortMode.None);
        // Add to the list for code use.
        winCrates.AddRange(tempCrates);
        recordedCrates.AddRange(tempCrates);
        // Log the find.
        Debug.Log($"Found Crates: {tempCrates.Length}!");

    }

    protected override void Update()
    {
        base.Update();

        // First if statement; if there are still winCrates in the list.
        if (winCrates.Count > 0)
        {
            CrateSent = false;
            // Loop through all available crates.
            for (int x = 0; x < winCrates.Count; x++)
            {
                // If one of those crates is in position, 
                if (winCrates[x].PosX == PosX && winCrates[x].PosY == PosY)
                {
                    // Play sound.
                    if (soundEffectOne.Length > 0)
                    {
                        PlaySound(soundEffectOne);
                    }
                    else
                    {
                        Debug.Log("Sound effect one missing on: " + gameObject.name);
                    }
                    SendQueryCrate(winCrates[x]);
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

        // If there's a player and the crates are gone...
        if (player && CrateSent)
        {
            // Play sound.
            if (soundEffectTwo.Length > 0)
            {
                PlaySound(soundEffectTwo);
            }
            else
            {
                Debug.Log("Sound effect two missing on: " + gameObject.name);
            }

            //Deactivate the player
            player.gameObject.SetActive(false);
            // Changes grid value to 0.
            gt.grid.SetValue(PosX, PosY, (0));
            // Mark that a player has left.
            PlayersRemaining -= 1;
            // Empty variable so that it doesn't just go "Player wins!" immediately.
            player = null;

            // If no players are left, Clear Level.
            if (PlayersRemaining == 0)
            {
                gm.ClearLevel();
            }
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
                StartCoroutine(inCrate.RemoveChest());
                // Remove from the list.
                winCrates.RemoveAt(x);
                // Changes grid value to 0.
                gt.grid.SetValue(PosX, PosY, (0));
                // Log the change.
                Debug.Log($"Crates left: {winCrates.Count}!");
            }
        }
    }

    public override void ResetPosition()
    {

        winCrates.Clear();
        winCrates.AddRange(recordedCrates);

        PlayersRemaining = 2;
        P1.gameObject.SetActive(true);
        P2.gameObject.SetActive(true);

        P1.ResetPos();
        P2.ResetPos();

        for (int x = 0; x < winCrates.Count; x++)
        {
            winCrates[x].gameObject.SetActive(true);
            winCrates[x].ResetPosition();
        }
    }
}
