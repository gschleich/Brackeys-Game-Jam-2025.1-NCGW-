using UnityEngine;
using TMPro;

public class TurretUpgradeManager : MonoBehaviour
{
    public static TurretUpgradeManager Instance; // Singleton instance

    public int turretRangeLevel = 1;
    public int turretFireRateLevel = 1;

    public float baseRange = 5f;
    public float baseFireRate = 1f;

    public TextMeshProUGUI rangeLevelText; // Assign UI text for range level
    public TextMeshProUGUI fireRateLevelText; // Assign UI text for fire rate level

    private ScrapText scrapManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Obsolete]
    private void Start()
    {
        scrapManager = FindObjectOfType<ScrapText>();
        UpdateUpgradeTexts();
    }

    public float GetUpgradedRange()
    {
        return baseRange + (turretRangeLevel - 1); // Increase range by 1 per level
    }

    public float GetUpgradedFireRate()
    {
        return baseFireRate + (turretFireRateLevel - 1); // Increase fire rate by 1 per level
    }

    public void UpgradeRange()
    {
        if (scrapManager.HasEnoughScrap(40))
        {
            scrapManager.UseScrap(40);
            turretRangeLevel++;
            UpdateUpgradeTexts();
        }
    }

    public void UpgradeFireRate()
    {
        if (scrapManager.HasEnoughScrap(50))
        {
            scrapManager.UseScrap(50);
            turretFireRateLevel++;
            UpdateUpgradeTexts();
        }
    }

    private void UpdateUpgradeTexts()
    {
        rangeLevelText.text = $"LVL. {turretRangeLevel}";
        fireRateLevelText.text = $"LVL. {turretFireRateLevel}";
    }
}
