using UnityEngine;

public class Enemy : MonoBehaviour
{
    public WaveManager waveManager;

    [System.Obsolete]
    void Start()
    {
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
    }
}
