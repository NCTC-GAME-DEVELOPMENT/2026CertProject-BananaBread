using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class GameManager : Info
{
    public static GameManager instance;
    public bool IsResetTriggered = false;

    private PlayerController P1;
    private PlayerController P2;
    private Stopwatch stopwatch;
    private GameObject ClearScreen;
    private TextMeshProUGUI FinalTime;

    private void Start()
    {
        P1 = GameObject.Find("P1").GetComponent<PlayerController>();
        P2 = GameObject.Find("P2").GetComponent<PlayerController>();
        stopwatch = GameObject.Find("StopwatchManager").GetComponent<Stopwatch>();
        ClearScreen = GameObject.Find("ClearScreen");
        FinalTime = GameObject.Find("FinalTime").GetComponent<TextMeshProUGUI>();

        ClearScreen.SetActive(false);
    }

    private void Update()
    {
        FailChecker();

        if (IsResetTriggered)
        {
            StartCoroutine(ReturnResetOff());
        }
    }

    public void StartGame()
    {
        stopwatch.stopwatchActive = true;
        P1.IsActive = true;
        P2.IsActive = true;
    }

    public void ClearLevel()
    {
        //When a level is cleared, calls this function
        stopwatch.stopwatchActive = false;
        FinalTime.text = stopwatch.FinalTimeText();
        ClearScreen.SetActive(true);
    }

    //If Both players are 'Caught', Game Over!
    public void FailChecker()
    {
        if (P1.IsCaught && P2.IsCaught)
        {
            //Debug.Log("GAME OVER!");
        }
    }

    IEnumerator ReturnResetOff()
    {
        //Debug.Log("Reset Successful");
        yield return new WaitForSeconds(0.25f);
        IsResetTriggered = false;
    }
}