using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    Stopwatch stopwatch;

    public TextMeshProUGUI HighScoreText;
    public GameObject RecordText;

    public Button MenuButton;

    private void Start()
    {
        if (GameObject.Find("StopwatchManager"))
        {
            stopwatch = GameObject.Find("StopwatchManager").GetComponent<Stopwatch>();
        }

        else { stopwatch = GameObject.Find("StopwatchManager(Clone)").GetComponent<Stopwatch>(); }

        HighScoreText.text = stopwatch.FinalTimeText();

        if (stopwatch.BestTime > stopwatch.FinalTime || stopwatch.BestTime == 0)
        {
            RecordText.SetActive(true);
        }

        else { RecordText.SetActive(false);}

        stopwatch.SetHighScore();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
