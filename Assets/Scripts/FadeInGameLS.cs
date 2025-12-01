using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInGameLS : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f;
 
   private void OnEnable()
{
    if (fadeImage == null)
    {
        Debug.LogError("FadeInGameLS: No fadeImage assigned.", this);
        return;
    }

    // Make sure the image object is on and visible
    fadeImage.gameObject.SetActive(true);

    // Force starting alpha to 1
    Color c = fadeImage.color;
    c.a = 1f;
    fadeImage.color = c;

    // Ensure canvas is on top
    Canvas canvas = fadeImage.canvas;
    if (canvas != null)
    {
        canvas.sortingOrder = 9999;
    }

    StartCoroutine(FadeOutAndDeactivate());
}

private IEnumerator FadeOutAndDeactivate()
{
    float elapsed = 0f;
    Color c = fadeImage.color;

    while (elapsed < fadeDuration)
    {
        elapsed += Time.deltaTime;

        c.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
        fadeImage.color = c;

        yield return null;
    }

    // Ensure it’s fully transparent
    c.a = 0f;
    fadeImage.color = c;

    // Turn off object when done
    fadeImage.gameObject.SetActive(false);
}
 
   
}

