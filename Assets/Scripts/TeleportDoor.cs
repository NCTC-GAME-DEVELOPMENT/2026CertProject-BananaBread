
using UnityEngine;

// Plays sound effect one when teleporting crates or player.

public class TeleportDoor : Common
{

    public TeleportDoor destinationDoor;
    bool crateTeleported = false;

    //Grid_testing variable.

    // Array for all crates in level.
    private Crate[] crates;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        // 0 is up, 90 is right, -90 is left, 180 is down.
        base.Start();

        // Get the grid.
        gt = GameObject.Find("GameManager").GetComponent<Grid_testing>();


        // Code for moving self to center wall of grid space.
        // It is troublesome and doesn't work cleanly..
        // But it's the best solution I could come up with.
        Vector3 location = gameObject.transform.position = gt.grid.GetWorldPosition(PosX, PosY);
        if(Facing == currentDirection.West)
        {
            location.z = location.z + (gt.cellSize / 2f);
            location.x = location.x + (gt.cellSize);
        }
        else if (Facing == currentDirection.East)
        {
            location.z = location.z + (gt.cellSize / 2f);
        }
        else if (Facing == currentDirection.North)
        {
            location.x = location.x + (gt.cellSize /2f);
        }
        else
        {
            location.x = location.x + (gt.cellSize / 2f);
            location.z = location.z + (gt.cellSize);

        }
        // Offset the GetWorldPosition Y zeroing.
        location.y = 1f;
        // Move to location.
        gameObject.transform.position = location;

        // Fillcrate array with all crates.
        crates = Object.FindObjectsByType<Crate>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }

    // Update for moving crates.
    protected override void Update()
    {

        // If both the door and destination are 0..
        if (gt.grid.GetValue(PosX, PosY) != 2 && gt.grid.GetValue(destinationDoor.PosX, destinationDoor.PosY) != 2)
        {
            // Set the crateTeleported variables back to false.
            crateTeleported = false;
            destinationDoor.crateTeleported = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if collission was player controller.
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        // If player controller, teleport to empty.
        if (player)
        {
            // Find destination.
            Vector3 destination = gt.grid.GetWorldPosition(destinationDoor.PosX, destinationDoor.PosY);
            // Get destination cell's value.
            int destinationSpace = gt.grid.GetValue(destinationDoor.PosX, destinationDoor.PosY);

            // Add half the cell size to center it on the grid cell.
            destination.x = destination.x + (gt.cellSize / 2f);
            destination.z = destination.z + (gt.cellSize / 2f);

            // If the grid is a 0 value...
            if (destinationSpace == 0)
            {
                // Teleport.
                movePlayer(player, destination);

            } // Else if there's a pushable block.
            else if (destinationSpace == 2)
            {
                // First, convert the facing direction to string.
                string doorFacing = destinationDoor.Facing.ToString();

                // Loop through the crates.
                for (int x = 0; x < crates.Length; x++)
                {
                    // Find the crate at the destination door.
                    if (crates[x].PosX == destinationDoor.PosX && crates[x].PosY == destinationDoor.PosY)
                    {
                        //Try to push it.
                        crates[x].MoveCrate(doorFacing);
                        Debug.Log("Crate pushed from door!");

                        // Re-obtain the value.
                        destinationSpace = gt.grid.GetValue(destinationDoor.PosX, destinationDoor.PosY);

                        // If it's empty now..
                        if (destinationSpace == 0)
                        {
                            // Play sound.
                            if (soundEffectOne != null)
                            {
                                PlaySound(soundEffectOne);
                            }
                            else
                            {
                                Debug.Log("Sound effect one missing on: " + gameObject.name);
                            }
                            // Teleport.
                            movePlayer(player, destination);

                        }
                    }
                }

            }
        }

    }

    // Function for crate teleportation.
    public void moveCrate(Crate crate)
    {
        //.. mark the destination as being teleported.
        destinationDoor.crateTeleported = true;
        // And self to keep further teleportations from happening.
        crateTeleported = true;

        // Find destination.
        Vector3 destination = gt.grid.GetWorldPosition(destinationDoor.PosX, destinationDoor.PosY);
        // Correct the Y so that it isn't embedded in the floor.
        destination.y = crate.SetYValue().y;

        // Add half the cell size to center it on the grid cell.
        destination.x = destination.x + (gt.cellSize / 2f);
        destination.z = destination.z + (gt.cellSize / 2f);

        // Play sound.
        if (soundEffectOne != null)
        {
            PlaySound(soundEffectOne);
        }
        else
        {
            Debug.Log("Sound effect one missing on: " + gameObject.name);
        }

        // Run the move function.
        crate.ExecuteTeleportation(destinationDoor.PosX, destinationDoor.PosY, destination, Facing.ToString());
    }

    // Made a separate function for readability and editability.
    private void movePlayer(PlayerController player, Vector3 destination)
    {

        // Set player positions to the destination door position.
        player.PosX = destinationDoor.PosX;
        player.PosY = destinationDoor.PosY;
        //Move them physically to the destination.
        player.gameObject.transform.position = destination;
        // Create a Quaternion for rotation.
        Quaternion newRotation;

        // If statements to rotate to the proper direction, and physically rotate the player.
        if(destinationDoor.Facing == currentDirection.North)
        {
            newRotation = Quaternion.Euler(0, 0, 0);
            player.gameObject.transform.rotation = newRotation;
        }
        else if (destinationDoor.Facing == currentDirection.South)
        {
            newRotation = Quaternion.Euler(0, 180, 0);
            player.gameObject.transform.rotation = newRotation;
        }
        else if (destinationDoor.Facing == currentDirection.East)
        {
            newRotation = Quaternion.Euler(0, 90, 0);
            player.gameObject.transform.rotation = newRotation;
        }
        else if (destinationDoor.Facing == currentDirection.West)
        {
            newRotation = Quaternion.Euler(0, -90, 0);
            player.gameObject.transform.rotation = newRotation;
        }
        // Play sound.
        if (soundEffectOne != null)
        {
            PlaySound(soundEffectOne);
        }
        else
        {
            Debug.Log("Sound effect one missing on: " + gameObject.name);
        }
        // Set teleporter grid value to open once done.
        base.gt.grid.SetValue(PosX, PosY, (0));
    }
    
}