using UnityEngine;

public class PressureSwitch : Common
{
    public SwitchGate[] Connections;
    //To Determine if a switch will only work if a player or a crate steps on it
    bool IsActive = false;
    public bool PlayersOnly = false;
    public bool CratesOnly = false;


    protected override void Start()
    {
        base.Start();
        CanReset = false;

        if (PlayersOnly && CratesOnly)
        {
            Debug.LogError(this + ": Cannot have PlayersOnly and CratesOnly be true at the same time!");
            PlayersOnly = false;
            CratesOnly = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        Crate crate = other.GetComponent<Crate>();

        if (!IsActive)
        {
            for (int c = 0; c < Connections.Length; c++)
            {
                if (pc && !CratesOnly)
                {
                    Connections[c].ToggleActivity();
                    IsActive = true;
                }
                if (crate && !PlayersOnly)
                {
                    Connections[c].ToggleActivity();
                    IsActive = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        Crate crate = other.GetComponent<Crate>();

        for (int c = 0; c < Connections.Length; c++)
        {
            if (gt.grid.GetValue(PosX, PosY) == 0)
            {
                if (pc && !CratesOnly)
                {
                    Debug.Log("Switch Stepped Off, Player");
                    Connections[c].ToggleActivity();
                    IsActive = false;
                }

                if (crate && !PlayersOnly)
                {
                    Debug.Log("Switch Stepped Off, Crate");
                    Connections[c].ToggleActivity();
                    IsActive = false;
                }
            }
        }
    }
}
