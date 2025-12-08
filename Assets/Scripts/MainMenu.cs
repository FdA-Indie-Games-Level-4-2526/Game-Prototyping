using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        // - Triggers the first level

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // - Closes the game

        Debug.Log("IT WORKS!!!!");
        Application.Quit();
    }
}
