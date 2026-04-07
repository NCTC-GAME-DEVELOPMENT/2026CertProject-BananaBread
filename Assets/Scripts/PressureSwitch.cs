using UnityEngine;

public class PressureSwitch : MonoBehaviour
{
    public SwitchGate Connection;

    //To Determine if a switch will only work if a player or a crate steps on it
    public bool PlayersOnly = false;
    public bool CratesOnly = false;

    private void Start()
    {
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

        if (pc && !CratesOnly)
        {
            Connection.ToggleActivity();
        }

        if (crate && !PlayersOnly)
        {
            Connection.ToggleActivity();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        Crate crate = other.GetComponent<Crate>();

        if (pc && !CratesOnly)
        {
            Debug.Log("Switch Stepped Off, Player");
            Connection.ToggleActivity();
        }

        if (crate && !PlayersOnly)
        {
            Debug.Log("Switch Stepped Off, Crate");
            Connection.ToggleActivity();
        }
    }
}
