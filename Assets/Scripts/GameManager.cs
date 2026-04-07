using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Info
{
    public static GameManager instance;
    public bool IsResetTriggered = false;

    private void Update()
    {
        if (IsResetTriggered)
        {
            StartCoroutine(ReturnResetOff());
        }

    }

    IEnumerator ReturnResetOff()
    {
        Debug.Log("Reset Successful");
        yield return new WaitForSeconds(0.25f);
        IsResetTriggered = false;
    }
}