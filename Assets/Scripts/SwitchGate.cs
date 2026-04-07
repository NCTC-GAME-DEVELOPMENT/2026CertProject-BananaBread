using UnityEngine;

public class SwitchGate : Common
{
    public GameObject Gate;
    public bool IsActive = false;
    private bool defaultState;

    protected override void Start()
    {
        gt = GameObject.Find("GameManager").GetComponent<Grid_testing>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartingPosition = gameObject.transform.position;

        defaultState = IsActive;
        FindStartCoordinates();
        RaiseLowerGate();
    }

    //Oversight: Player or Crate inside of the gate area if it raises again could break level. Find a fix soon.
    public void ToggleActivity()
    {
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
        IsActive = defaultState;
        RaiseLowerGate();
    }
}
