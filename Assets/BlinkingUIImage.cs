using UnityEngine;
using UnityEngine.UI;

public class BlinkingUIImage : MonoBehaviour
{
    public Image targetImage;           // Assign the UI Image in the Inspector
    public Color colorA = Color.white;  // First color
    public Color colorB = Color.black;    // Second color
    public float blinkSpeed = 1f;       // Speed of the blink

    private bool isBlinking = true;

    void Update()
    {
        if (isBlinking && targetImage != null)
        {
            float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            targetImage.color = Color.Lerp(colorA, colorB, t);
        }
    }

    public void StartBlinking() => isBlinking = true;

    public void StopBlinking()
    {
        isBlinking = false;
        targetImage.color = colorA; // Reset to default color
    }
}
