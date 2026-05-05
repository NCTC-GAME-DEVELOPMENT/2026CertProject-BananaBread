using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    public Button StartButton;
    public Button SelectLevelButton;
    public Button QuitButton;
    public Button CreditsButton;
    public Button BackButtonCredits;
    public Button BackButtonLevel;
    public string FirstLevel;
    public GameObject Credits;
    public GameObject LevelSelect;
    public GameObject MenuMain;


    private void Start()
    {
        MenuMain = GameObject.Find("MainMenu");
        Credits = GameObject.Find("CreditsMenu");
        LevelSelect = GameObject.Find("LevelMenu");

        MenuMain.SetActive(true);
        Credits.SetActive(false);
        LevelSelect.SetActive(false);
    }

    public void Button_start()
    {
        SceneManager.LoadScene(FirstLevel);
    }

    public void Button_selectLevel()
    {
        //SceneManager.LoadScene(LevelSelect);
        LevelSelect.SetActive(true);
        MenuMain.SetActive(false);
    }
    public void Button_Quit()
    {
        Application.Quit();
    }
    public void Button_Credits()
    {
        //SceneManager.LoadScene(Credits);
        Credits.SetActive(true);
        MenuMain.SetActive(false);
    }

    public void Button_Back()
    {
        MenuMain.SetActive(true);
        Credits.SetActive(false);
        LevelSelect.SetActive(false);
    }
}
