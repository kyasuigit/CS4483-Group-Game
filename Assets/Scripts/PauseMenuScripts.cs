using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScripts : MonoBehaviour
{

    public bool Paused = false;
    public GameObject PausedMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

   // Update is called once per frame
   void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Paused) {
                Play();
            }
            else {
                Stop();
            }
        }
    }

    void Stop() 
    {
        PausedMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;

    }

    public void Play() 
    {
        PausedMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}