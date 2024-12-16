using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField]
    private string targetSceneName; // The name of the scene to transition to

    [SerializeField]
    private float triggerDistance = 5f; // Distance at which the transition is triggered

    [SerializeField]
    private float fadeDuration = 1f; // Duration of the fade-out effect

    [SerializeField]
    private Image fadeImage; // Reference to the UI Image for fading

    private Camera mainCamera; // Reference to the main camera
    private bool isTransitioning = false; // Prevent double-triggering

    private void Start()
    {
        mainCamera = Camera.main;

        if (fadeImage != null)
        {
            // Ensure the fade image starts fully transparent
            fadeImage.color = new Color(0, 0, 0, 0);
        }
    }

    private void Update()
    {
        if (!isTransitioning && IsCameraClose())
        {
            TriggerSceneChange();
        }
    }

    private bool IsCameraClose()
    {
        if (mainCamera != null)
        {
            float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
            return distance <= triggerDistance;
        }
        return false;
    }

    private void TriggerSceneChange()
    {
        if (!isTransitioning)
        {
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

        // Load the target scene
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("Target scene name is not set on " + gameObject.name);
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
