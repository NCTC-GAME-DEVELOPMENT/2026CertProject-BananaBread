using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    /// <summary>
    /// Show Input for this controler, Super Spammy when true. 
    /// </summary>
    public bool LogInputStateInfo = false; 

    protected InputPoller inputPoller; 
    protected InputData InputCurrent;
    protected InputData InputPrevious;

    public bool PerformInputProcessing = true;

    Rigidbody rb;


    protected override void Start()
    {
        base.Start();
        IsHuman = true; 

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        inputPoller = InputPoller.Self; 
        if (!rb)
        {
            Debug.Log("No Rigidbody!");
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
        if (InputCurrent.buttonSouth)
        {
            Push(InputCurrent.buttonSouth);
        }

        Vertical(InputCurrent.leftStick.y);
        Horizontal(InputCurrent.leftStick.x);
    }



    public virtual void Horizontal(float value)
    {
        if (value != 0)
        {
            LOG("Del-Horizontal (" + value + ")");
        }
    }

    public virtual void Vertical(float value)
    {
        if (value != 0)
        {
            LOG("Del-Vertical (" + value +")");
        }       
    }

    public virtual void Push(bool value)
    {
        if (value)
        {
            LOG("PushButtonPressed");
        }
    }
}
