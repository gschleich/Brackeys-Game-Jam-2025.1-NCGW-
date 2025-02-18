using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Automatically find the "Wasteland Base" object in the scene
        GameObject baseObject = GameObject.Find("Wasteland Base");
        if (baseObject != null)
        {
            target = baseObject.transform; // Set target to Wasteland Base
        }
        else
        {
            Debug.LogError("Wasteland Base object not found in the scene!");
        }
    }

    void Update()
    {
        if (target == null) return;

        // Calculate direction to target
        Vector2 direction = (target.position - transform.position).normalized;
        movement = direction;
        
        // Flip sprite based on movement direction
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1); // Face right
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Face left
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        // Use the correct method for velocity update
        rb.linearVelocity = movement * speed; // Corrected from 'linearVelocity'
    }
}