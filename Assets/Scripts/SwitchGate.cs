using UnityEngine;

public class SwitchGate : Common
{
    public GameObject Gate;
    public bool IsActive = false;
    private bool DefaultState;

    protected override void Start()
    {
        gt = GameObject.Find("GameManager").GetComponent<Grid_testing>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartingPosition = gameObject.transform.position;

        DefaultState = IsActive;
        FindStartCoordinates();
        RaiseLowerGate();
    }

    //Find a way to implement this failsafe
    /*public bool IsGateSpaceOccupied()
    {
        if (gt.grid.GetValue(PosX, PosY) == 0 || gt.grid.GetValue(PosX, PosY) == 1)
        {
            return true;
        }

        return false;
    }*/

    public void ToggleActivity()
    {
        //while (!IsGateSpaceOccupied()) { }
            if (IsActive)
            {
                IsActive = false;
            }
            else
            {
                IsActive = true;
            }
            Debug.Log("Gate toggled, " + IsActive);
            RaiseLowerGate();
    }

    public void RaiseLowerGate()
    {
        if (IsActive)
        {
            Gate.SetActive(false);
            gt.grid.SetValue(PosX, PosY, (0));
        }
        else
        {
            Gate.SetActive(true);
            gt.grid.SetValue(PosX, PosY, (1));
        }
    }

    public override void ResetPosition()
    {
        IsActive = DefaultState;
        RaiseLowerGate();
    }
}
