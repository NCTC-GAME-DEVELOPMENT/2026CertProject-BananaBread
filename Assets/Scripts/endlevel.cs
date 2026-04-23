using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endlevel : MonoBehaviour
{
    private int MainMenu = 0;

    private Stopwatch stopwatch;
    private GameManager gm;

    public Button NextButton;
    public Button MenuButton;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        stopwatch = gm.stopwatch;
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
