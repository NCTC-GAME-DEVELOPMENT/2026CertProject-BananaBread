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
    public TextMeshProUGUI[] StopwatchText;
    public TextMeshProUGUI[] FinalTime;
    public GameObject GameManager;

    void Start()
    {
        currentTime = 0;
        stopwatchActive = false;
    }

    void Update()
    {
        if (stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
            if (currentTime >= 120 )
            {
                stopwatchActive = false;
                Debug.Log("Too much time taken! Game Failed!");
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        /*if (!GameManager.IsUsingKeyboard) {
            StopwatchText[0].text = time.ToString(@"m\:ss\:fff");
            StopwatchText[1].text = time.ToString(@"m\:ss\:fff");
        }
        else
        {
            StopwatchText[2].text = time.ToString(@"m\:ss\:fff");
        }

        if (!stopwatchActive)
        {
            if (!GameManager.IsUsingKeyboard) {
            FinalTime[0].text = time.ToString(@"m\:ss\:fff");
            FinalTime[1].text = time.ToString(@"m\:ss\:fff");
            }
            else
            {
                FinalTime[2].text = time.ToString(@"m\:ss\:fff");
            }
        }*/
    }
}
