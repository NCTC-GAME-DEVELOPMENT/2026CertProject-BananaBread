using UnityEngine;

// Plays soundEffectOne when IsActive. SoundEffectTwo when not.

public class SwitchGate : Common
{
    protected GateForce gf;
    public GameObject Gate;
    public GameObject ForceVolume;
    public bool IsActive = false;
    protected bool DefaultState;

    Animator anim;

    protected override void Start()
    {
        gt = GameObject.Find("GameManager").GetComponent<Grid_testing>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gf = GetComponentInChildren<GateForce>();
        anim = GetComponentInChildren<Animator>();
        StartingPosition = gameObject.transform.position;
        gf.myPosition = StartingPosition;

        DefaultState = IsActive;

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
            // Play sound.
            PlaySound(soundEffectOne);
            anim.SetBool("SwitchDown", true);
            Gate.SetActive(false);
            ForceVolume.SetActive(false);
            gt.grid.SetValue(PosX, PosY, (0));
        }
        else
        {
            // Play sound.
            PlaySound(soundEffectTwo);
            anim.SetBool("SwitchDown", false );
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
