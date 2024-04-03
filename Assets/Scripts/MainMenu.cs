using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool guardianSelected = false;
    private string nextSceneName;

    public void selectGuardian()
    {
        PlayerPrefs.SetString("character", "Guardian");
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    public void selectAssassin()
    {
        PlayerPrefs.SetString("character", "Assassin");
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
