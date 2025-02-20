using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scrap : MonoBehaviour, ICollectible
{
    public static event Action OnScrapCollected;
    Rigidbody2D rb;

    bool hasTarget;
    Vector3 targetPosition;
    float moveSpeed = 5f;
    float lifetime = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    public void Collect()
    {
        Debug.Log("Scrap Collected");
        Destroy(gameObject);
        OnScrapCollected?.Invoke();
    }

    private void FixedUpdate()
    {
        if(hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.linearVelocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }

    public void OnApplicationQuit(){
        Destroy(gameObject);
    }
}
