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
        PlayerChoice.CharacterChoice = "Guardian";
        SceneManager.LoadScene(1);
    }

    public void selectAssassin()
    {
        PlayerChoice.CharacterChoice = "Assassin";
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
