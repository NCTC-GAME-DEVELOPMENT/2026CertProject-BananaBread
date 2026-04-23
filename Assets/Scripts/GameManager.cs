using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameManager : Info
{
    public static GameManager instance;
    public bool IsResetTriggered = false;
    public int CurrentScene;

    private PlayerController P1;
    private PlayerController P2;
    public Stopwatch stopwatch;

    private GameObject ClearScreen;
    public TextMeshProUGUI FinalTime;
    public TextMeshProUGUI LevelTime;
    public GameObject StopwatchManager;

    private void Start()
    {
        if (GameObject.Find("StopwatchManager") == false)
        {
            Debug.Log("Stopwatch not detected. Making one now");
            GameObject.Instantiate(StopwatchManager);
            stopwatch = GameObject.Find("StopwatchManager(Clone)").GetComponent<Stopwatch>();
        }
        else { stopwatch = GameObject.Find("StopwatchManager").GetComponent<Stopwatch>(); }

        P1 = GameObject.Find("P1").GetComponent<PlayerController>();
        P2 = GameObject.Find("P2").GetComponent<PlayerController>();
        ClearScreen = GameObject.Find("ClearScreen");
        FinalTime = GameObject.Find("FinalTime").GetComponent<TextMeshProUGUI>();
        LevelTime = GameObject.Find("LevelTime").GetComponent<TextMeshProUGUI>();

        stopwatch.ConfigureSceneSettings();
        stopwatch.currentTime = 0;
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


    //When a level is cleared, calls this function
    public void ClearLevel()
    {
        stopwatch.stopwatchActive = false;
        ClearScreen.SetActive(true);

        GameObject lt = GameObject.Find("LevelTime");

        if (stopwatch.FinalTime == 0)
        {
            lt.SetActive(false);
        }

        LevelTime.text = ("+" + stopwatch.LevelTimeText());

        stopwatch.LevelCleared();
        FinalTime.text = stopwatch.FinalTimeText();
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
        yield return new WaitForSeconds(0.1f);
        IsResetTriggered = false;
    }
}