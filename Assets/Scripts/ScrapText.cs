using TMPro;
using UnityEngine;

public class ScrapText : MonoBehaviour
{
    public TextMeshProUGUI scrapText;
    public int scrapCount;

    private void OnEnable()
    {
        Scrap.OnScrapCollected += IncrementScrapCount;
    }

    private void OnDisable()
    {
        Scrap.OnScrapCollected -= IncrementScrapCount;
    }

    public void IncrementScrapCount()
    {
        scrapCount++;
        scrapText.text = $"{scrapCount}";
    }

    public bool HasEnoughScrap(int amount)
    {
        return scrapCount >= amount;
    }

    public void UseScrap(int amount)
    {
        scrapCount -= amount;
        scrapText.text = $"{scrapCount}";
    }
}
