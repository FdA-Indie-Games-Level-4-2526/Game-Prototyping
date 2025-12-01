using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
   // [SerializeField] Animator transitionAnim; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
       StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        // transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene(). buildIndex + 1);
    }
}
