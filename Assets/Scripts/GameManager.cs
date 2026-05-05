using System.Collections;
using TMPro;
using UnityEngine;

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
        // Get the camera.
        Camera mainCam = Camera.main;
        // Get grid height for math
        float height = GameObject.Find("GameManager").GetComponent<Grid_testing>().height;
        // Do math for Y location: height * 7 solves it for the 6/8/10 sizes.
        // Appears to work at 12 too, but the X location breaks at that point.
        float newY = height * 7f;
        // Rations for X location:
        // X10 = -50 X location
        // X8 = -32 X locatoin
        // X6 = -22 X location
        // The newX equation appears to solve that.
        // At 12, it is slightly off, should be closer to map, but map seeable.
        float newX = -(height * height) + (9f * height) - 40f;
        // Apply equations to the camera location.
        mainCam.transform.position = new Vector3(0f, newY, newX);
        // With location solved, time to point the camera at the GameManager.
        // Get game manager minus camera location.
        Vector3 direction = transform.position - mainCam.transform.position;
        // Create a rotation looking that way.
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // Extract only the X rotation.
        Vector3 rotation = Vector3.zero;
        rotation.x = lookRotation.x;
        // Apply rotation to camera.
        mainCam.transform.rotation = lookRotation;
        // As long as you stick to the 6/8/10 sizes, it will be fine.
        // Sizes in between would probably work as well.


        //If a StopwatchManager does not exist, makes one
        if (GameObject.Find("StopwatchManager") == false && GameObject.Find("StopwatchManager(Clone)") == false)
        {
            Debug.Log("Stopwatch not detected. Making one now");
            GameObject.Instantiate(StopwatchManager);
            stopwatch = GameObject.Find("StopwatchManager(Clone)").GetComponent<Stopwatch>();
        }
        else if (GameObject.Find("StopwatchManager") == true) { stopwatch = GameObject.Find("StopwatchManager").GetComponent<Stopwatch>(); }
        else { stopwatch = GameObject.Find("StopwatchManager(Clone)").GetComponent<Stopwatch>(); }

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
        //FailChecker();

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

        P1.CanReset = false;
        P2.CanReset = false;

        if (stopwatch.FinalTime == 0)
        {
            lt.SetActive(false);
        }

        LevelTime.text = ("+" + stopwatch.LevelTimeText());

        stopwatch.LevelCleared();
        FinalTime.text = stopwatch.FinalTimeText();
    }

    //If Both players are 'Caught', Game Over!
    /*public void FailChecker()
    {
        if (P1.IsCaught && P2.IsCaught)
        {
            //Debug.Log("GAME OVER!");
        }
    }*/

    IEnumerator ReturnResetOff()
    {
        //Debug.Log("Reset Successful");
        yield return new WaitForSeconds(0.1f);
        IsResetTriggered = false;
    }
}