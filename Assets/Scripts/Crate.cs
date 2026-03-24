using UnityEngine;

public class Crate : MonoBehaviour
{
    public int[] currentPos;
    public float moveAmount = 3.0f;
    Vector3 myPosition;

    //PlayerController senses a crate to push in front of it.
    //If success, pushes the crate in its facing direction, moving x or y based on the current push direction.

    private void Start()
    {
        myPosition = gameObject.transform.position;
    }
    public void MoveCrate(string direction)
    {
        Debug.Log("Push Detected");
        if (direction == "North")
        {
            gameObject.transform.position = new Vector3(myPosition.x, myPosition.y, (myPosition.z + moveAmount));
        }
        else if (direction == "South")
        {
            gameObject.transform.position = new Vector3(myPosition.x, myPosition.y, (myPosition.z - moveAmount));
        }
        else if (direction == "East")
        {
            gameObject.transform.position = new Vector3((myPosition.x + moveAmount), myPosition.y, myPosition.z);
        }
        else
        {
            gameObject.transform.position = new Vector3((myPosition.x - moveAmount), myPosition.y, myPosition.z);
        }
    }
}
