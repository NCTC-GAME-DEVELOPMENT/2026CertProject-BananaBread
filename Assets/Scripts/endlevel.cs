using UnityEngine;
using UnityEngine.SceneManagement;

public class endlevel : MonoBehaviour
{
    public string nextLevel;
    private int MainMenu = 0;

    private Stopwatch stopwatch;
    private GameManager gm;

    private void Start()
    {
        stopwatch = GameObject.Find("StopwatchManager").GetComponent<Stopwatch>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Button_NextLevel()
    {
        Debug.Log("NextLevel Pressed");

        gm.CurrentScene += 1;
        SceneManager.LoadScene(gm.CurrentScene);
    }

    public void Button_mainMenu()
    {
        Debug.Log("mainMenu Pressed");

        stopwatch.currentTime = 0;
        stopwatch.FinalTime = 0;

        SceneManager.LoadScene(MainMenu);
    }
}
