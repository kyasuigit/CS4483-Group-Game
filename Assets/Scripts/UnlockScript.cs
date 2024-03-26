using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject unlockAttack;
    public GameObject unlockKnockback;
    public GameObject unlockMovespeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        unlockAttack.SetActive(false);
        unlockKnockback.SetActive(false);
        unlockMovespeed.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void Pause()
    {
        unlockAttack.SetActive(true);
        unlockKnockback.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Debug.Log("Game paused");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
