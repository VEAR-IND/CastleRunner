using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour {
    public void Bioms()
    {
        SceneManager.LoadScene(3);
    }
    public void Characters()
    {        
        
    }
    public void Weapon()
    {

    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
