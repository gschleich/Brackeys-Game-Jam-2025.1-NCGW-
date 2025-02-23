using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;
    [SerializeField] FloatingHealthBar healthBar;
    public GameObject scrapMetalPrefab;
    public GameObject[] weaponPrefabs; // Assign weapons prefabs in the Inspector
    public WaveManager waveManager;
    public float attack = 1f;

    [System.Obsolete]
    void Awake()
    {
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

        // Ignore collisions between Enemy and Turret/Player layers
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int turretLayer = LayerMask.NameToLayer("Turret");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, turretLayer, true);
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, true);
    }

    void OnDestroy()
    {
        Die();
        SpawnScrapMetal();
        TryDropWeapon();
    }

    void SpawnScrapMetal()
    {
        if (scrapMetalPrefab != null)
        {
            Instantiate(scrapMetalPrefab, transform.position, Quaternion.identity);
        }
    }
    
    void TryDropWeapon()
    {
        float dropChance = Random.value; // Generates a random number between 0 and 1

        if (dropChance <= 0.10f) // 10% chance
        {
            if (weaponPrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, weaponPrefabs.Length); // Pick a random weapon
                Instantiate(weaponPrefabs[randomIndex], transform.position, Quaternion.identity);
                Debug.Log("Weapon Dropped!");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        SoundManager.Instance.PlaySound2D("EnemyTakeDamage");
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if(health <= 0){
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died. Notifying WaveManager.");
        
        if (waveManager != null) // Ensure waveManager exists to prevent errors
        {
            waveManager.EnemyDefeated(gameObject); // Now properly passing this enemy
        }
        else
        {
            Debug.LogError("WaveManager reference is missing on enemy: " + gameObject.name);
        }

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

    public float GetAttack(){
        return attack;
    }
}
