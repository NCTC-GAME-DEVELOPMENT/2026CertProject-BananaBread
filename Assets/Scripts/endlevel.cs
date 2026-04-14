using UnityEngine;
using UnityEngine.SceneManagement;

public class endlevel : MonoBehaviour
{
    public string nextLevel;
    public string MainMenu;
    public void Button_NextLevel()
    {
        Debug.Log("NextLevel Pressed");
        SceneManager.LoadScene(nextLevel);
    }
    public void Button_mainMenu()
    {
        Debug.Log("mainMenu Pressed");
        SceneManager.LoadScene(MainMenu);
    }
}
