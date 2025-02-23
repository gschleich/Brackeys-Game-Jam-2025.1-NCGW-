using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public enum GameState { Preparation, Wave, GameOver }
    public GameState currentState = GameState.Preparation;

    public float preparationTime = 10f;
    public float waveTime = 30f;
    public int waveNumber = 1;
    
    public Transform[] spawnPoints; 
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject UIControls; // Reference for the UI Controls
    public GarageDoorController garageDoor; // Add this reference in the Inspector
    public int baseEnemyCount = 5;

    [Header("Debug Info")]
    public int currentEnemiesAlive;

    private bool isWaveActive;
    private HashSet<GameObject> activeEnemies = new HashSet<GameObject>(); // Track active enemies

    void Start()
    {
        StartCoroutine(PreparationPhase());
    }

    void Update()
    {
        if (currentState == GameState.Wave && activeEnemies.Count == 0 && isWaveActive)
        {
            isWaveActive = false;
            Debug.Log("All enemies defeated! Starting preparation phase...");
            StartCoroutine(PreparationPhase());
        }
    }

    IEnumerator PreparationPhase()
    {
        MusicManager.Instance.PlayMusic("PrepPhase");
        currentState = GameState.Preparation;
        UIControls.SetActive(true); // Enable UI controls during prep phase
        Debug.Log($"Preparation Phase - Wave {waveNumber} starts in {preparationTime} seconds.");

        // Close the garage door at the start of preparation
        garageDoor.CloseDoor();

        yield return new WaitForSeconds(preparationTime);

        StartCoroutine(WavePhase());
    }

    IEnumerator WavePhase()
    {
        MusicManager.Instance.PlayMusic("WavePhase");
        currentState = GameState.Wave;
        UIControls.SetActive(false); // Disable UI controls during wave phase
        isWaveActive = true;
        activeEnemies.Clear();
        int enemiesToSpawn = baseEnemyCount + (waveNumber * 2);
        currentEnemiesAlive = enemiesToSpawn;

        // Open the garage door at the start of the wave
        garageDoor.OpenDoor();

        Debug.Log($"Wave {waveNumber} started! Spawning {enemiesToSpawn} enemies.");

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }

        waveNumber++;
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyToSpawn = (waveNumber > 3 && Random.value <= 0.33f) ? enemyPrefab2 : enemyPrefab;
        
        GameObject enemy = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
        activeEnemies.Add(enemy); // Add enemy to tracking list
        
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.waveManager = this;
        }
        else
        {
            Debug.LogError("Spawned enemy does not have an Enemy script attached!");
        }
    }

    public void EnemyDefeated(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy)) // Ensure enemy is in the tracking list
        {
            activeEnemies.Remove(enemy); // Remove from tracking set
            currentEnemiesAlive = activeEnemies.Count; // Update inspector value
            Debug.Log($"Enemy defeated! Remaining enemies: {currentEnemiesAlive}");
        }

        if (activeEnemies.Count == 0 && isWaveActive)
        {
            Debug.Log("All enemies defeated! Returning to preparation phase.");
            isWaveActive = false;
            StartCoroutine(PreparationPhase());
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over! Restart or Exit.");
        currentState = GameState.GameOver;
    }
}
