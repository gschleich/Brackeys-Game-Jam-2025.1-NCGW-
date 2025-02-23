using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public TMP_Text textComponent;  // Assign in Inspector
    public Color color1 = Color.white;
    public Color color2 = Color.black;
    public float blinkInterval = 0.5f;  // Time between switches in seconds

    private float timer;
    private bool isColor1 = true;

    private void Update()
    {
        if (textComponent != null)
        {
            timer += Time.deltaTime;

            if (timer >= blinkInterval)
            {
                // Toggle color
                isColor1 = !isColor1;
                textComponent.color = isColor1 ? color1 : color2;

                // Reset timer
                timer = 0f;
            }
        }
    }
}
