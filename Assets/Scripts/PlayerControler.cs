using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : Controller
{
    public static PlayerController instance;

    public PushTrigger pt;

    public bool LogInputStateInfo = false;

    public float MoveSpeed = 1.0f;

    

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
        if (InputCurrent.buttonEast)
        {
            Push(InputCurrent.buttonEast);
        }

        PlayerMovement(InputCurrent.leftStick);
    }

    //Tests for which direction the player moves in, then rotates the player accordingly.
    public virtual void PlayerMovement(Vector2 value)
    {
        
        if (Mathf.Abs(value.x) > Mathf.Abs(value.y))
        {
            if (value.x > 0)
            {
                Facing = currentDirection.East;
                Debug.Log("P" + PlayerNumber + " direction: " + Facing);
                rb.linearVelocity = gameObject.transform.forward * value.x * MoveSpeed;
                rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else
            {
                Facing = currentDirection.West;
                Debug.Log("P" + PlayerNumber + " direction: " + Facing);
                rb.linearVelocity = gameObject.transform.forward * (value.x * -1) * MoveSpeed;
                rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            }
        }
        else
        {
            if (value.y > 0)
            {
                Facing = currentDirection.North;
                Debug.Log("P" + PlayerNumber + " direction: " + Facing);
                rb.linearVelocity = gameObject.transform.forward * value.y * MoveSpeed;
                rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                rb.linearVelocity = gameObject.transform.forward * (value.y * -1) * MoveSpeed;
                if (value.y < 0) 
                {
                    rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    Facing = currentDirection.South;
                    Debug.Log("P" + PlayerNumber + " direction: " + Facing);
                }
            }
        }
    }

    public virtual void Push(bool value)
    {
        if (value)
        {
            LOG("Push Push");
            StartCoroutine(pt.PushAction());
        }
    }
}
