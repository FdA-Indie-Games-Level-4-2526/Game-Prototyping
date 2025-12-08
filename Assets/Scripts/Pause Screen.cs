using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
  public static bool GameIsPaused = false;

  public GameObject pauseScreenUI;


  void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // - Opens the Pause Screen

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
              Pause();  
            }
        }
    }

    void Resume()
    {
        // - Unpaused Settings

        pauseScreenUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        // - Paused Settings
        pauseScreenUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
