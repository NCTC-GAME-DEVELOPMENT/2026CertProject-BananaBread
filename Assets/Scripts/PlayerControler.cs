using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

// Plays sound effect one when isMoving.
// Plays sound effect two when isPushing.

public class PlayerController : Controller
{
    public static PlayerController instance;
    public PushTrigger pt;
    private Grid_testing gt;
    private GameManager gm;

    public Animator anim;
    public bool LogInputStateInfo = false;
    public bool IsCaught = false;
    public bool IsActive = false;
    private bool IsPushing = false;
    private bool IsMoving = false;
    public float MoveSpeed = 1.0f;

    public int PosX;
    public int PosY;
    private Vector3 StartingPosition;
    private int StartX;
    private int StartY;

    private AudioSource sounds;

    public AudioClip[] soundEffectOne;
    public AudioClip[] soundEffectTwo;

    public float pitchShiftLowRange = 1f;
    public float pitchShiftHighRange = 1f;


    public enum currentDirection
    {
        North,
        East,
        South,
        West
    }

    public currentDirection Facing = currentDirection.East;
    protected InputPoller inputPoller; 
    protected InputData InputCurrent;
    protected InputData InputPrevious;

    public bool PerformInputProcessing = true;

    Rigidbody rb;

    protected override void Start()
    {
        base.Start();
        IsHuman = true;

        sounds = GetComponentInChildren<AudioSource>();

        anim = GetComponentInChildren<Animator>();
        gt = GameObject.Find("GameManager").GetComponent<Grid_testing>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        FindStartCoordinates();
        gt.grid.SetValue(PosX, PosY, (3));

        rb = gameObject.GetComponent<Rigidbody>();
        inputPoller = InputPoller.Self; 
        if (!rb)
        {
            Debug.Log("Rigidbody Not Found");
        }
        else
        {
            Debug.Log("Rigidbody Found");
            
        }
        
        if (!inputPoller)
        {
            LOG_ERROR("****PLAYER CONTROLER: No Input Poller in Scene");
            return; 
        }
    }
    protected void Update()
    {
        GetInput();
        if(PerformInputProcessing)
        {
            ProcessInput();
        }
        InputPrevious = InputCurrent;

        gt.grid.SetValue(PosX, PosY, (3));

        PlayerGridMovement();
        // Do nothing if no sounds or if a sound is playing.
        if (sounds == null){ }
        else if (sounds.isPlaying) { }
        // If ismoving, play sound effect.
        else if (IsMoving && soundEffectOne.Length > 0)
        {
            PlaySound(soundEffectOne);
        }
        else if (soundEffectOne.Length == 0)
        {
            Debug.Log("Sound effect one missing on: " + gameObject.name);
        }

    }

    //Uses the object's world position to set its starting coordinates
    public void FindStartCoordinates()
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

    //Configures the grid's values based on the player's current position
    public void PlayerGridMovement()
    {
        Vector3 gridPos = gt.grid.GetWorldPosition(PosX, PosY) + new Vector3(gt.cellSize, 0, gt.cellSize) * .5f;
        Vector3 myPos = gameObject.transform.position;

        if (!IsCaught && IsActive)
        {
            if (myPos.z > gridPos.z + 1.5)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                PosY += 1;
            }
            if (myPos.z < gridPos.z - 1.5)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                PosY -= 1;
            }
            if (myPos.x > gridPos.x + 1.5)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                PosX += 1;
            }
            if (myPos.x < gridPos.x - 1.5)
            {
                gt.grid.SetValue(PosX, PosY, (0));
                PosX -= 1;
            }
        }
    }

    protected virtual void GetInput()
    {
        if (!InputPoller.Self)
        {
            LOG_ERROR("****PLAYER CONTROLER (" + gameObject.name + "): No Input Poller in Scene");
            return;
        }
        
        InputCurrent = InputPoller.Self.GetInput(InputPlayerNumber);

        if (LogInputStateInfo)
        {
            LOG(InputCurrent.ToString());
        }
       
    }

    protected virtual void ProcessInput()
    {
        if (!IsCaught && IsActive && !IsPushing)
        {
            if (InputCurrent.buttonEast)
            {
                Push(InputCurrent.buttonEast);
            }
            if (InputCurrent.selectButton)
            {
                ResetLevel(InputCurrent.selectButton);
            }
            
            PlayerMovement(InputCurrent.leftStick);
        }
    }

    //Tests for which direction the player moves in, then rotates the player accordingly.
    public virtual void PlayerMovement(Vector2 value)
    {
        if (!IsPushing)
        {
        if (Mathf.Abs(value.x) > Mathf.Abs(value.y))
        {
            anim.SetBool("IsMoving", true);
            IsMoving = true;
            if (value.x > 0)
            {
                Facing = currentDirection.East;
                //Debug.Log("P" + PlayerNumber + " direction: " + Facing);
                rb.linearVelocity = gameObject.transform.forward * value.x * MoveSpeed;
                rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else
            {
                Facing = currentDirection.West;
                //Debug.Log("P" + PlayerNumber + " direction: " + Facing);
                rb.linearVelocity = gameObject.transform.forward * (value.x * -1) * MoveSpeed;
                rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            }
        }
        else
        {
            if (value.y > 0)
            {
                anim.SetBool("IsMoving", true);
                IsMoving = true;
                Facing = currentDirection.North;
                //Debug.Log("P" + PlayerNumber + " direction: " + Facing);
                rb.linearVelocity = gameObject.transform.forward * value.y * MoveSpeed;
                rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                anim.SetBool("IsMoving", false);
                IsMoving = false;
                rb.linearVelocity = gameObject.transform.forward * (value.y * -1) * MoveSpeed;
                if (value.y < 0)
                {
                    anim.SetBool("IsMoving", true);
                    IsMoving = true;
                    rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    Facing = currentDirection.South;
                    //Debug.Log("P" + PlayerNumber + " direction: " + Facing);
                }
            }
        }
        }
    }

    public virtual void Push(bool value)
    {
        if (value)
        {
            // Play sound.
            if (soundEffectTwo.Length > 0)
            {
                PlaySound(soundEffectTwo);
            }
            else
            {
                Debug.Log("Sound effect two missing on: " + gameObject.name);
            }
            LOG("Push Push");
            IsPushing = true;
            anim.SetBool("IsPushing", true);
            StartCoroutine(Cooldown(0.4f));
            StartCoroutine(pt.PushAction());
        }
    }

    public void ResetLevel(bool value)
    {
        gm.IsResetTriggered = true;
        gt.grid.SetValue(PosX, PosY, (0));
        gameObject.transform.position = new Vector3(StartingPosition.x, StartingPosition.y, StartingPosition.z);
        PosX = StartX;
        PosY = StartY;
        gt.grid.SetValue(PosX, PosY, (3));

        rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        Facing = currentDirection.East;
    }

    IEnumerator Cooldown(float value)
    {
        yield return new WaitForSeconds(value);
        anim.SetBool("IsPushing", false);
        IsPushing = false;
    }

    public virtual void PlaySound(AudioClip[] sound)
    {
        int index = Random.Range(0, sound.Length);
        sounds.clip = sound[index];
        sounds.pitch = Random.Range(pitchShiftLowRange, pitchShiftHighRange);
        sounds.Play();
    }
}
