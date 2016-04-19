using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Continue()
    {

    }
    public void New()
    {
        Debug.Log("load");
        SceneManager.LoadScene(1);
    }
    public void Shop()
    {
        SceneManager.LoadScene(2);
    }
    public void Options()
    {
        PlayerPrefs.DeleteAll();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
