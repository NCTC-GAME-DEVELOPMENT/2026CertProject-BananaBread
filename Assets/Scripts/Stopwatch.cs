using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public bool stopwatchActive = false;
    public float currentTime = 0;
    public float FinalTime = 0;
    float BestTime = 0;
    public TextMeshProUGUI StopwatchText;
    public GameManager gm;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void ConfigureSceneSettings()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        StopwatchText = GameObject.Find("Stopwatch").GetComponent<TextMeshProUGUI>();
        Debug.Log("Successfully Set GameManager and Stopwatch text");
    }

    void Update()
    {
        if (stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
            if (currentTime >= 5999 )
            {
                stopwatchActive = false;
                Debug.Log("Too much time taken! Game Failed!");
            }
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        StopwatchText.text = time.ToString(@"mm\:ss\:fff");

        if (gm.IsResetTriggered)
        {
            currentTime = 0;
        }
    }

    public void LevelCleared()
    {
        stopwatchActive = false;
        FinalTime += currentTime;
    }


    public void SetHighScore()
    {
        if (BestTime > FinalTime)
        {
            BestTime = FinalTime;
        }
    }

    public string LevelTimeText()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        return time.ToString(@"mm\:ss\:fff");
    }

    public string FinalTimeText()
    {
        TimeSpan time = TimeSpan.FromSeconds(FinalTime);
        return time.ToString(@"mm\:ss\:fff");
    }
}
