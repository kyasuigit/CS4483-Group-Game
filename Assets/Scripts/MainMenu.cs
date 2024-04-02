using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool guardianSelected = false;
    private string nextSceneName;

    public IEnumerator selectGuardian()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(1f);

        guardianSelected = true;
        GameObject.Find("Assassin").SetActive(false);
        GameObject.Find("Guardian").SetActive(true);

        Debug.Log("Disable assassin");

    }

    public IEnumerator selectAssassin()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(1f);

        GameObject.Find("Guardian").SetActive(false);
        GameObject.Find("Assassin").SetActive(true);
        Debug.Log("Disable Guardian");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has quit the game");
    }
}
