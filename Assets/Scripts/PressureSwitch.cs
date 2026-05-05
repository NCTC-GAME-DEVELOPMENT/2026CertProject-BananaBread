using UnityEngine;

// Plays sound effect one when switch down.
// Plays sound effect two when not.

public class PressureSwitch : Common
{
    public SwitchGate[] Connections;
    bool IsActive = false;

    //To Determine if a switch will only work if a player or a crate steps on it
    public bool PlayersOnly = false;
    public bool CratesOnly = false;

    Animator anim;

    protected override void Start()
    {
        base.Start();
        CanReset = false;
        anim = GetComponent<Animator>();

        if (PlayersOnly && CratesOnly)
        {
            Debug.LogError(this + ": Cannot have PlayersOnly and CratesOnly be true at the same time!");
            PlayersOnly = false;
            CratesOnly = false;
        }
    }

    //For some reason, the code will only functions properly when the pc and crate conditions are two separate IF statements, so I had to revert the changes.
    //However, I condensed the code into one function to have less repeated code.
    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        Crate crate = other.GetComponent<Crate>();

        if (!IsActive)
        {
            if ((pc && !CratesOnly))
            {
                SwitchOnOff(true, soundEffectOne);
            }

            if ((crate && !PlayersOnly))
            {
                SwitchOnOff(true, soundEffectOne);
            }

                for (int c = 0; c < Connections.Length; c++)
                {
                    if ((pc && !CratesOnly))
                    {
                        Connections[c].ToggleActivity();
                    }
                    if (crate && !PlayersOnly)
                    {
                        Connections[c].ToggleActivity();
                    }
                }
            }
        }

    private void OnTriggerExit(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        Crate crate = other.GetComponent<Crate>();
        
        if ((pc && !CratesOnly) && gt.grid.GetValue(PosX, PosY) == 0)
        {
            SwitchOnOff(false, soundEffectTwo);
        }

        if ((crate && !PlayersOnly) && gt.grid.GetValue(PosX, PosY) == 0)
        {
            SwitchOnOff(false, soundEffectTwo);
        }

        for (int c = 0; c < Connections.Length; c++)
        {
            if (gt.grid.GetValue(PosX, PosY) == 0)
            {
                if ((pc && !CratesOnly))
                {
                    Connections[c].ToggleActivity();
                }

                if (crate && !PlayersOnly)
                {
                    Connections[c].ToggleActivity();
                }
            }
        }
    }

    private void SwitchOnOff(bool status, AudioClip[] sound)
    {
        string debugNum;

        if (status)
        {
            debugNum = "one";
        }
        else
        {
            debugNum = "two";
        }

        // Play sound.
        if (sound.Length > 0)
        {
            PlaySound(sound);
        }
        else
        {
            Debug.Log("Sound effect " + debugNum + " missing on: " + gameObject.name);
        }

        anim.SetBool("SwitchDown", status);
        IsActive = status;
        Debug.Log("Is Active: " + IsActive);
    }
}
