using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    public Transform target; // Assign the target in the Inspector or dynamically
    public SpriteRenderer spriteRenderer; // Assign the turret's sprite renderer
    
    void Update()
    {
        if (target == null) return;

        // Calculate direction to target
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Flip the sprite based on the rotation
        spriteRenderer.flipY = angle > 90 || angle < -90;
    }
}