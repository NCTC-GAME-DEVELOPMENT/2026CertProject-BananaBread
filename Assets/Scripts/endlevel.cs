using UnityEngine;
using UnityEngine.SceneManagement;

public class endlevel : MonoBehaviour
{
    public string nextLevel;
    public string MainMenu;

    private Stopwatch stopwatch;
    public void Button_NextLevel()
    {
        Debug.Log("NextLevel Pressed");
        SceneManager.LoadScene(nextLevel);
    }
    public void Button_mainMenu()
    {
        Debug.Log("mainMenu Pressed");

        stopwatch.currentTime = 0;
        stopwatch.FinalTime = 0;

        SceneManager.LoadScene(MainMenu);
    }
}
