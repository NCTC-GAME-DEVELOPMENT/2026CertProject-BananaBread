using UnityEngine;

public class Common : MonoBehaviour
{
    protected Grid_testing gt;
    protected GameManager gm;

    protected Vector3 myPosition;
    public int PosX;
    public int PosY;
    protected int GridValue;

    protected Vector3 StartingPosition;
    protected int StartX;
    protected int StartY;

    public enum currentDirection
    {
        None,
        North,
        East,
        South,
        West,
    }

    public currentDirection Facing = currentDirection.None;
    public bool CanReset = true;

    protected virtual void Start()
    {
        gt = GameObject.Find("GameManager").GetComponent<Grid_testing>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartingPosition = gameObject.transform.position;

        FindStartCoordinates();
        myPosition = gameObject.transform.position;
    }

    protected virtual void Update()
    {
        if (gm.IsResetTriggered && CanReset)
        {
            ResetPosition();
        }
    }

    public virtual void ChangeLocation(int newX, int newY, Vector3 newLocation)
    {
        // Set value to 0.
        gt.grid.SetValue(PosX, PosY, (0));
        // Alter PosX, PosY, and myPosition.
        PosX = newX;
        PosY = newY;
        // Set the new position to the grid value.
        gt.grid.SetValue(PosX, PosY, (GridValue));
        //Physically move it.
        gameObject.transform.position = newLocation;
        // change myPosition to the new location.
        myPosition = newLocation;
    }

    /*
     *                 
     *          anim.SetBool("PushWest", true);
                gt.grid.SetValue(PosX, PosY, (0));
                PosX -= 1;
                gt.grid.SetValue(PosX, PosY, (GridValue));
                gameObject.transform.position = new Vector3((myPosition.x - moveAmount), myPosition.y, myPosition.z);
                myPosition = gameObject.transform.position;
                StartCoroutine(Cooldown(0.5f));*/

    //Uses the object's world position to set its starting coordinates
    public virtual void FindStartCoordinates()
    {
        StartingPosition = gameObject.transform.position;

        //Find the Position of the X Variable
        float x = (((StartingPosition.x - 1.5f) / 3) + (gt.width / 2));
        PosX = (int)x;
        StartX = PosX;

        //Do the same for the Y Variable
        float y = (((StartingPosition.z - 1.5f) / 3) + (gt.height / 2));
        PosY = (int)y;
        StartY = PosY;
    }

    public virtual void ResetPosition()
    {
        gt.grid.SetValue(PosX, PosY, (0));
        gameObject.transform.position = new Vector3(StartingPosition.x, StartingPosition.y, StartingPosition.z);
        myPosition = gameObject.transform.position;

        PosX = StartX;
        PosY = StartY;
        gt.grid.SetValue(PosX, PosY, (GridValue));
    }
}
