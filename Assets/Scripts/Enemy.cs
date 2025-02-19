using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;
    [SerializeField] FloatingHealthBar healthBar;
    public GameObject scrapMetalPrefab;
    public WaveManager waveManager;
    // public float damage = 1f;

    [System.Obsolete]
    void Awake(){
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        if (healthBar == null)
        {
            Debug.LogError("FloatingHealthBar component not found on enemy.");
        }
    }
    void Start()
    {
        health = maxHealth;
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(health, maxHealth);
        }
        // Find the WaveManager in the scene automatically
        waveManager = FindObjectOfType<WaveManager>();

        if (waveManager == null)
        {
            Debug.LogError("WaveManager not found in the scene!");
        }
    }

    void OnDestroy()
    {
        if (waveManager != null)
        {
            waveManager.EnemyDefeated();
        }
        SpawnScrapMetal();
    }

    void SpawnScrapMetal()
    {
        if (scrapMetalPrefab != null)
        {
            Instantiate(scrapMetalPrefab, transform.position, Quaternion.identity);
        }
    }

    public void TakeDamage(float damage){
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if(health <= 0){
            Die();
        }
    }
    void Die(){
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) // Ensure Enemy has the "Enemy" tag
        {
            BulletController bullet = other.GetComponent<BulletController>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }

            Destroy(other.gameObject); // Destroy bullet on impact
        }
    }
}
