using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : Controller
{
    public static PlayerController instance;

    public bool LogInputStateInfo = false;

    public float MoveSpeed = 1.0f;

    public char currentDirection = 'n';
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
        if (Mathf.Abs(value.x) > Mathf.Abs(value.y))
        {
            if (value.x > 0)
            {
                currentDirection = 'e';
                rb.linearVelocity = gameObject.transform.forward * value.x * MoveSpeed;
                rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else
            {
                currentDirection = 'w';
                rb.linearVelocity = gameObject.transform.forward * (value.x * -1) * MoveSpeed;
                rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            }
        }
        else
        {
            if (value.y > 0)
            {
                currentDirection = 'n';
                rb.linearVelocity = gameObject.transform.forward * value.y * MoveSpeed;
                rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                rb.linearVelocity = gameObject.transform.forward * (value.y * -1) * MoveSpeed;
                if (value.y < 0) 
                {
                    rb.rotation = gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    currentDirection = 's';
                }
            }
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
