using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string targetSceneName; // Name of the scene to load

    [SerializeField]
    private float fadeDuration = 1f; // Duration of the fade-out effect

    [SerializeField]
    private Image fadeImage; // Reference to the UI Image for fading

    private bool isTransitioning = false; // Prevent double-triggering

    private void Awake()
    {
        if (fadeImage != null)
        {
            // Ensure the fade image starts fully transparent
            fadeImage.color = new Color(0, 0, 0, 0);
        }
    }

    /// <summary>
    /// Starts the scene transition.
    /// </summary>
    public void TriggerSceneChange()
    {
        if (!isTransitioning)
        {
            StartCoroutine(ChangeSceneWithFade());
        }
    }

    /// <summary>
    /// Starts the scene transition with a specific target scene.
    /// </summary>
    /// <param name=\"newSceneName\">Name of the scene to load.</param>
    public void TriggerSceneChange(string newSceneName)
    {
        if (!isTransitioning)
        {
            targetSceneName = newSceneName; // Dynamically update the scene name
            StartCoroutine(ChangeSceneWithFade());
        }
    }

    private System.Collections.IEnumerator ChangeSceneWithFade()
    {
        isTransitioning = true;

        if (fadeImage != null)
        {
            // Perform the fade-out effect
            yield return StartCoroutine(FadeOut());
        }

        // Load the new scene
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("Target scene name is not set.");
        }
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        // Gradually increase the alpha value of the fade image
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
