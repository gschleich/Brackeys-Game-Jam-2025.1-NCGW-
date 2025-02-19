using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrapText : MonoBehaviour
{
    public TextMeshProUGUI scrapText;
    int scrapCount;

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
}
