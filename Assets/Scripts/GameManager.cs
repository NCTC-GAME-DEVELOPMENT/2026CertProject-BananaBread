using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Info
{
    public static GameManager instance;
    public bool IsResetTriggered = false;

    private PlayerController P1;
    private PlayerController P2;

    private void Start()
    {
        P1 = GameObject.Find("P1").GetComponent<PlayerController>();
        P2 = GameObject.Find("P2").GetComponent<PlayerController>();
    }

    private void Update()
    {
        FailChecker();

        if (IsResetTriggered)
        {
            StartCoroutine(ReturnResetOff());
        }
    }

    //If Both players are 'Caught', Game Over!
    public void FailChecker()
    {
        if (P1.IsCaught && P2.IsCaught)
        {
            Debug.Log("GAME OVER!");
        }
    }

    IEnumerator ReturnResetOff()
    {
        Debug.Log("Reset Successful");
        yield return new WaitForSeconds(0.25f);
        IsResetTriggered = false;
    }
}