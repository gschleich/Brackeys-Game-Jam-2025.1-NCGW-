using UnityEngine;
using UnityEngine.UI;

public class Barricade : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 6;
    [SerializeField] private int currentHealth;

    [Header("UI Settings")]
    public Image healthBarImage;
    public Sprite[] healthBarSprites;

    [Header("Collider Settings")]
    public BoxCollider2D[] healthColliders; // Assign different colliders per health level
    private BoxCollider2D currentCollider;

    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        currentCollider = GetComponent<BoxCollider2D>();
        UpdateHealthUI();
        UpdateCollider();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();
        UpdateAnimation();
        UpdateCollider();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public bool Repair(int amount)
    {
        if (currentHealth >= maxHealth)
            return false; // No repair needed

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();
        UpdateAnimation();
        UpdateCollider();

        return true; // Successfully repaired
    }

    private void UpdateHealthUI()
    {
        if (healthBarImage != null && healthBarSprites.Length == 6)
        {
            int spriteIndex = Mathf.Clamp(currentHealth - 1, 0, 5);
            healthBarImage.sprite = healthBarSprites[spriteIndex];
        }
    }

    private void UpdateAnimation()
    {
        animator.SetInteger("Health", currentHealth);
    }

    private void UpdateCollider()
    {
        if (healthColliders.Length == 6 && currentCollider != null)
        {
            int colliderIndex = Mathf.Clamp(currentHealth - 1, 0, 5);
            BoxCollider2D newCollider = healthColliders[colliderIndex];

            if (newCollider != null)
            {
                currentCollider.size = newCollider.size;
                currentCollider.offset = newCollider.offset;
            }
        }
    }
}
