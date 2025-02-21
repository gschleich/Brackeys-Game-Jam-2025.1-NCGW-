using UnityEngine;

public class SpriteToggle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Cache the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure the GameObject has a SpriteRenderer component
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject. Please add one.");
        }
    }

    // Method to toggle the sprite's visibility
    public void ToggleSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }
}
