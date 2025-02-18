using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    public Transform player; // Reference to the player
    public SpriteRenderer spriteRenderer; // Weapon's sprite renderer

    void Update()
    {
        if (player == null || spriteRenderer == null) return;

        // Get the position of the mouse in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the Z position remains consistent

        // Calculate the direction from the player to the mouse
        Vector3 direction = mousePosition - player.position;

        // Calculate the angle to rotate the weapon towards the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Flip the weapon sprite horizontally based on the mouse's position relative to the player
        spriteRenderer.flipX = direction.x < 0;
    }
}