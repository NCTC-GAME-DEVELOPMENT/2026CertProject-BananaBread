using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : Controller
{
    /// <summary>
    /// Show Input for this controler, Super Spammy when true. 
    /// </summary>
    public bool LogInputStateInfo = false;

    public float MoveSpeed = 1.0f;

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

    public virtual void PlayerMovement(Vector2 value)
    {
        //rb.linearVelocity = gameObject.transform.right * value * MoveSpeed;
        //rb.linearVelocity = gameObject.transform.forward * value * MoveSpeed;

        if (Mathf.Abs(value.x) > Mathf.Abs(value.y))
        {
            rb.linearVelocity = gameObject.transform.right * value.x * MoveSpeed;
        }
        else
        {
            rb.linearVelocity = gameObject.transform.forward * value.y * MoveSpeed;
        }
    }

    public virtual void Push(bool value)
    {
        if (value)
        {
            LOG("Push Push");
        }
    }
}
