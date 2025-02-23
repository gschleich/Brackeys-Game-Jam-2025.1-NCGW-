using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseHealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public Collider2D baseCollider;
    public GameObject objectToToggle; // GameObject to enable/disable when health reaches 0
    private bool isGameOver = false;

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

        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false); // Make sure it's initially inactive
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
        if (healthAmount <= 0 && !isGameOver)
        {
            SoundManager.Instance.PlaySound2D("BaseExplode");
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(true); // Activate the object when health hits 0
            }
            isGameOver = true; // Prevent reactivating or reloading the scene multiple times
            // Optionally, reload the scene after a delay:
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Max(healthAmount, 0);
        healthBar.fillAmount = healthAmount / 100f;
    }
}
