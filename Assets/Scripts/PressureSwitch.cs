using UnityEngine;

// Plays sound effect one when switch down.
// Plays sound effect two when not.

public class PressureSwitch : Common
{
    public SwitchGate[] Connections;
    //To Determine if a switch will only work if a player or a crate steps on it
    bool IsActive = false;
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

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        Crate crate = other.GetComponent<Crate>();

        if (!IsActive)
        {
            if ((pc && !CratesOnly) || (crate && !PlayersOnly))
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
                anim.SetBool("SwitchDown", true);

                for (int c = 0; c < Connections.Length; c++)
                {   // Two if statements were identical in function, merged into one OR condition.
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
        
        if ((pc && !CratesOnly) || (crate && !PlayersOnly))
        {
            // Play sound.
            if (soundEffectTwo != null)
            {
                PlaySound(soundEffectTwo);
            }
            else
            {
                Debug.Log("Sound effect two missing on: " + gameObject.name);
            }
            anim.SetBool("SwitchDown", false);
        }

        for (int c = 0; c < Connections.Length; c++)
        {
            if (gt.grid.GetValue(PosX, PosY) == 0)
            {   // Same duplicate if with different conditions here.
                if ((pc && !CratesOnly) || (crate && !PlayersOnly))
                {
                    Debug.Log("Switch Stepped Off, Player");
                    Connections[c].ToggleActivity();
                    IsActive = false;
                }
            }
        }
    }
}
