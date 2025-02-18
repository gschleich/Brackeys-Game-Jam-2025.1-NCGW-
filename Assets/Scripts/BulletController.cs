using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 1f; //damage value
    public float speed = 5f; // Bullet speed
    public float lifetime = 3f; //Destroy bullet after some time

    void Start()
    {
        SetDirectionAndRotation();
        Destroy(gameObject, lifetime); //destroy after lifetime
    }

    void SetDirectionAndRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - transform.position).normalized;

        // Rotate the bullet to face the mouse, assuming the right side is the forward direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }

    
}
