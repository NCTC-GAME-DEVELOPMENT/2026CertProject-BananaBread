using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    

   

    public Button StartButton;
    public Button SelectLevelButton;
    public Button QuitButton;
    public Button CreditsButton;
    public string Credits;
    public string FirstLevel;
    public string LevelSelect;
    public string Quit;




    public void Button_start()
    {
        SceneManager.LoadScene(FirstLevel);
    }

    public void Button_selectLevel()
    {
        SceneManager.LoadScene(LevelSelect);
    }
    public void Button_Quit()
    {
        SceneManager.LoadScene(Quit);
    }
    public void Button_Credits()
    {
       SceneManager.LoadScene(Credits);
    }
}
