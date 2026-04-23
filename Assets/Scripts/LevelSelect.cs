using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSlect : MonoBehaviour
{




    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;
    public Button level6;
    public string first;
    public string second;
    public string third;
    public string fourth;
    public string fifth;
    public string sixth;




    public void Button_Level1()
    {
        SceneManager.LoadScene(first);
    }

    public void Button_Level2()
    {
        SceneManager.LoadScene(second);
    }
    public void Button_Level3()
    {
        SceneManager.LoadScene(third);
    }
    public void Button_Level4()
    {
       SceneManager.LoadScene(fourth);
    }
    public void Button_Level5()
    {
        SceneManager.LoadScene(fifth);
    }
    public void Button_Level6()
    {
        SceneManager.LoadScene(sixth);
    }
}
