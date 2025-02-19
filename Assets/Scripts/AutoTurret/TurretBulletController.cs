using UnityEngine;

public class TurretBulletController : MonoBehaviour
{
    public float damage = 1f; // Bullet damage
    public float speed = 5f; // Bullet speed
    public float lifetime = 3f; // Destroy bullet after some time
    private Vector2 direction; // Bullet movement direction

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy bullet after some time
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
        
        // Rotate the bullet to face the movement direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        // Apply velocity
        GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); // Destroy bullet on impact
        }
    }
}

