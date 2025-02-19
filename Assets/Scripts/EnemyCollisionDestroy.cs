using UnityEngine;

public class EnemyCollisionDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided with the trigger has the tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Destroy the enemy object
            Destroy(other.gameObject);
        }
    }
}