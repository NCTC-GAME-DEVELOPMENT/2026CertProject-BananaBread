using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endlevel : MonoBehaviour
{
    private Stopwatch stopwatch;
    private GameManager gm;

    public Button NextButton;
    public Button MenuButton;

    public AudioClip ClearAnnouncer;
    public AudioClip ButtonClicker;
    private AudioSource sound;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sound = GetComponent<AudioSource>();
        sound.clip = ClearAnnouncer;
        sound.Play();
    }

    public void Button_NextLevel()
    {
        Debug.Log("NextLevel Pressed");

        sound.clip = ButtonClicker;
        gm.CurrentScene += 1;
        SceneManager.LoadScene(gm.CurrentScene);
    }

    public void Button_mainMenu()
    {
        Debug.Log("mainMenu Pressed");

        sound.clip = ButtonClicker;
        stopwatch = gm.stopwatch;
        stopwatch.currentTime = 0;
        stopwatch.FinalTime = 0;

        SceneManager.LoadScene("start_Menu_test");
    }
}
