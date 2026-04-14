using System;
using System.Collections;
using UnityEngine;

public class SwitchGate : Common
{
    protected GateForce gf;
    public GameObject Gate;
    public GameObject ForceVolume;
    public bool IsActive = false;
    protected bool DefaultState;

    protected override void Start()
    {
        gt = GameObject.Find("GameManager").GetComponent<Grid_testing>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gf = GetComponentInChildren<GateForce>();
        StartingPosition = gameObject.transform.position;
        gf.myPosition = StartingPosition;

        DefaultState = IsActive;

        if (Facing == currentDirection.North || Facing == currentDirection.South)
        {
            Gate.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        FindStartCoordinates();
        RaiseLowerGate();
    }

    protected override void Update()
    {
        base.Update();

        if (!IsActive)
        {
            gt.grid.SetValue(PosX, PosY, (1));
        }
    }

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
            ForceVolume.SetActive(false);
            gt.grid.SetValue(PosX, PosY, (0));
        }
        else
        {
            Gate.SetActive(true);
            ForceVolume.SetActive(true);
            gt.grid.SetValue(PosX, PosY, (1));
        }
    }

    public override void ResetPosition()
    {
        IsActive = DefaultState;
        RaiseLowerGate();
    }
}
