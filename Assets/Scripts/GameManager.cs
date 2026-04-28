using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
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

    // For the camera.
    private Camera mainCam;
    // Grabbed only for the camera math.
    private Grid_testing gt;

    private void Start()
    {
        /*
         * 
         * Didn't get it working yet.
         * 
         * 
        // Get the grid.
        gt = GameObject.Find("GameManager").GetComponent<Grid_testing>();

        // Get the camera.
        mainCam = Camera.main;

        // The math is: with about a 2:1 level design, 7 to the camera's Y per 2:1 makes it viewable.
        // So, if I grab X of the grid and multiply it by 7, we get a useable Y height for the camera.
        // May as well set the Z and rotation by default as well.
        // -61.4 Z, and 51.9 X rotation. May as well see how those look rounded.

        // Let's try this.
        // Get camera minus game manager location.
        Vector3 direction = gt.transform.position - mainCam.transform.position;
        // Create a rotation towards that direction.
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // Extract only the X rotation.
        Vector3 rotation = transform.eulerAngles;
        rotation.x = lookRotation.eulerAngles.x * 1.5f; // Times 1.5 because that works better.
        mainCam.transform.eulerAngles = rotation;



        mainCam.transform.position = new Vector3(0f, (gt.height * 7f), (gt.height * -3.5f));
        */

        if (GameObject.Find("StopwatchManager") == false && GameObject.Find("StopwatchManager(Clone)") == false)
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