using UnityEngine;
using UnityEngine.UI;

public class Barricade : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 6; // Now supports 6 HP
    [SerializeField] private int currentHealth; // Visible in Inspector

    [Header("UI Settings")]
    public Image healthBarImage; // Assign in Inspector
    public Sprite[] healthBarSprites; // Drag all 6 sprites in order from empty to full

    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) 
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

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Repair(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();
        UpdateAnimation();
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
}
