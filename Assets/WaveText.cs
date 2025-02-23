using System.Collections;
using UnityEngine;
using TMPro;

public class WaveText : MonoBehaviour
{
    public GameObject waveUI; // Reference to the Wave UI GameObject
    public TextMeshProUGUI waveText; // Reference to the TextMeshPro UI Text for displaying the wave number
    private CanvasGroup canvasGroup; // Reference to the CanvasGroup for fading

    void Start()
    {
        // Ensure CanvasGroup is attached to the WaveUI GameObject
        canvasGroup = waveUI.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = waveUI.AddComponent<CanvasGroup>(); // Add one if it doesn't exist
        }
    }

    public void StartWaveTextDisplay(int waveNumber)
    {
        StartCoroutine(DisplayWaveText(waveNumber));
    }

    IEnumerator DisplayWaveText(int waveNumber)
    {
        waveUI.SetActive(true); // Activate the Wave UI
        waveText.text = "Wave " + waveNumber; // Set the text to display the current wave number
        canvasGroup.alpha = 1f; // Ensure the UI starts fully visible
        yield return null; // Wait one frame to ensure the UI is active before starting the fade

        // Wait for 1 second while the UI is visible
        yield return new WaitForSeconds(2f);

        // Fade out the UI over 1 second
        float fadeDuration = 1f;
        float startAlpha = canvasGroup.alpha;
        float endAlpha = 0f;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha; // Ensure alpha is set to 0
        waveUI.SetActive(false); // Deactivate the Wave UI
    }
}
