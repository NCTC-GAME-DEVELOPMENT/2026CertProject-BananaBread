using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public bool stopwatchActive;
    public float currentTime;
    public int startTime;
    public TextMeshProUGUI StopwatchText;
    public TextMeshProUGUI FinalTime;
    public GameObject gm;

    void Start()
    {
        gm = GameObject.Find("GameManager");
        StopwatchText = GameObject.Find("Stopwatch").GetComponent<TextMeshProUGUI>();
        FinalTime = GameObject.Find("FinalTime").GetComponent<TextMeshProUGUI>();
        currentTime = 0;
        stopwatchActive = false;
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

        StopwatchText.text = time.ToString(@"m\:ss\:fff");

        FinalTime.text = time.ToString(@"m\:ss\:fff");
    }

    public void PauseBetweenLevels()
    {
        stopwatchActive = false;
    }

    public string FinalTimeText()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        return time.ToString(@"m\:ss\:fff");
    }
}
