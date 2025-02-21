using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        // Listen for scene reload
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public void Collect()
    {
        Debug.Log("Scrap Collected");
        Destroy(gameObject);
        OnScrapCollected?.Invoke();
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.linearVelocity = targetDirection * moveSpeed;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }

    private void OnSceneUnloaded(Scene current)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}
