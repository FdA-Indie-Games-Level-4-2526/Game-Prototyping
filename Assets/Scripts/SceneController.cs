using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [SerializeField] Animator transitionAnim;
    [SerializeField] FadingScript fadingScript;

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

        if (fadingScript == null)
        {
            fadingScript = GetComponent<FadingScript>();
        }
    }

    public void NextLevel()
    {
        // - Initiates load level function

       StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        // - Handles the transitions between the levels

        //  transitionAnim.SetTrigger("End");
        fadingScript.FadeOut();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene(). buildIndex + 1);
        fadingScript.FadeIn();
        Debug.Log("Loading");
    }
}
