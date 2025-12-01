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
        pauseScreenUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseScreenUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
