using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseHealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public Collider2D baseCollider;

    private void Start()
    {
        if (baseCollider == null)
        {
            baseCollider = GetComponent<Collider2D>();
        }

        if (baseCollider != null)
        {
            baseCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    void Update()
    {
        if (healthAmount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Max(healthAmount, 0);
        healthBar.fillAmount = healthAmount / 100f;
    }
}
