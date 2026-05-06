using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    public Button StartButton;
    public Button PlayButton;
    public Button SelectLevelButton;
    public Button QuitButton;
    public Button CreditsButton;
    public Button BackButtonCredits;
    public Button BackButtonLevel;
    public string FirstLevel;
    public GameObject MenuMain;
    public GameObject HowToPlay;
    public GameObject Credits;
    public GameObject LevelSelect;
    public AudioClip ButtonClickSound;
    private AudioSource sound;

    private void Start()
    {
        MenuMain = GameObject.Find("MainMenu");
        HowToPlay = GameObject.Find("HowToMenu");
        Credits = GameObject.Find("CreditsMenu");
        LevelSelect = GameObject.Find("LevelMenu");
        sound = GetComponent<AudioSource>();

        sound.clip = ButtonClickSound;

        MenuMain.SetActive(true);
        HowToPlay.SetActive(false);
        Credits.SetActive(false);
        LevelSelect.SetActive(false);
    }

    public void Button_start()
    {
        if (GameObject.Find("StopwatchManager(Clone)") == true)
        {
            SceneManager.LoadScene(FirstLevel);
        }
        else
        {
            MenuMain.SetActive(false);
            HowToPlay.SetActive(true);
        }

        sound.Play();
    }

    public void Button_Play()
    {
        SceneManager.LoadScene(FirstLevel);
        sound.Play();
    }

    public void Button_selectLevel()
    {
        //SceneManager.LoadScene(LevelSelect);
        LevelSelect.SetActive(true);
        MenuMain.SetActive(false);
        sound.Play();
    }
    public void Button_Quit()
    {
        sound.Play();
        Application.Quit();
    }
    public void Button_Credits()
    {
        //SceneManager.LoadScene(Credits);
        Credits.SetActive(true);
        MenuMain.SetActive(false);
        sound.Play();
    }

    public void Button_Back()
    {
        MenuMain.SetActive(true);
        Credits.SetActive(false);
        LevelSelect.SetActive(false);
        sound.Play();
    }
}
