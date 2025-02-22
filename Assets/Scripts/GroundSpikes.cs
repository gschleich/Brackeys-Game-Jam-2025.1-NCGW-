using UnityEngine;

public class GroundSpikes : MonoBehaviour
{
    [SerializeField] private int spikeHealth = 4;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateSpikeAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Deal 1 damage to the enemy
                ReduceSpikeHealth();
            }
        }
    }

    private void ReduceSpikeHealth()
    {
        spikeHealth--;

        if (spikeHealth <= 0)
        {
            Destroy(gameObject); // Destroy spikes when health reaches 0
        }
        else
        {
            UpdateSpikeAnimation();
        }
    }

    private void UpdateSpikeAnimation()
    {
        switch (spikeHealth)
        {
            case 4:
                animator.Play("Spikes1");
                break;
            case 3:
                animator.Play("Spikes2");
                break;
            case 2:
                animator.Play("Spikes3");
                break;
            case 1:
                animator.Play("Spikes4");
                break;
        }
    }
}
