using UnityEngine;

public class Crate : MonoBehaviour
{
    public Grid_testing gt;
    public int PosX;
    public int PosY;
    Vector3 myPosition;
    float moveAmount = 3.0f;

    //PlayerController senses a crate to push in front of it.
    //If success, pushes the crate in its facing direction, moving x or y based on the current push direction.

    private void Start()
    {
        gt.grid.SetValue(PosX, PosY, (2));
        myPosition = gameObject.transform.position;
    }
    public void MoveCrate(string direction)
    {
        Debug.Log("Push Detected");
        if (CanPushHere(direction))
        {
            if (direction == "North")
            {
                gt.grid.SetValue(PosX, PosY, (0));
                PosY += 1;
                gt.grid.SetValue(PosX, PosY, (2));
                gameObject.transform.position = new Vector3(myPosition.x, myPosition.y, (myPosition.z + moveAmount));
                myPosition = gameObject.transform.position;
            }
            else if (direction == "South")
            {
                gt.grid.SetValue(PosX, PosY, (0));
                PosY -= 1;
                gt.grid.SetValue(PosX, PosY, (2));
                gameObject.transform.position = new Vector3(myPosition.x, myPosition.y, (myPosition.z - moveAmount));
                myPosition = gameObject.transform.position;
            }
            else if (direction == "East")
            {
                gt.grid.SetValue(PosX, PosY, (0));
                PosX += 1;
                gt.grid.SetValue(PosX, PosY, (2));
                gameObject.transform.position = new Vector3((myPosition.x + moveAmount), myPosition.y, myPosition.z);
                myPosition = gameObject.transform.position;
            }
            else
            {
                gt.grid.SetValue(PosX, PosY, (0));
                PosX -= 1;
                gt.grid.SetValue(PosX, PosY, (2));
                gameObject.transform.position = new Vector3((myPosition.x - moveAmount), myPosition.y, myPosition.z);
                myPosition = gameObject.transform.position;
            }
        }
    }

    protected bool CanPushHere(string direction)
    {
        if (direction == "North" && gt.grid.GetValue(PosX, (PosY + 1)) == 0)
        {
            return true;
        }
        if (direction == "South" && gt.grid.GetValue(PosX, (PosY - 1)) == 0)
        {
            return true;
        }
        if (direction == "East" && gt.grid.GetValue((PosX + 1), PosY) == 0)
        {
            return true;
        }
        if (direction == "West" && gt.grid.GetValue((PosX - 1), PosY) == 0)
        {
            return true;
        }
        else
        {
            Debug.Log("You cannot push a crate here!");
            return false;
        }

    }
}
