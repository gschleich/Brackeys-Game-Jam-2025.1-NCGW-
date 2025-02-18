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
    
    public Transform[] spawnPoints; // Assign in inspector
    public GameObject enemyPrefab; // Assign enemy prefab
    public int baseEnemyCount = 5; // Base enemy count per wave

    private int currentEnemies;
    private bool isWaveActive;

    void Start()
    {
        StartCoroutine(PreparationPhase());
    }

    void Update()
    {
        if (currentState == GameState.Wave && currentEnemies <= 0 && isWaveActive)
        {
            isWaveActive = false;
            StartCoroutine(PreparationPhase());
        }
    }

    IEnumerator PreparationPhase()
    {
        currentState = GameState.Preparation;
        Debug.Log($"Preparation Phase - Wave {waveNumber} starts in {preparationTime} seconds.");
        
        yield return new WaitForSeconds(preparationTime);
        
        StartCoroutine(WavePhase());
    }

    IEnumerator WavePhase()
    {
        currentState = GameState.Wave;
        isWaveActive = true;
        currentEnemies = baseEnemyCount + (waveNumber * 2); // Increase enemy count per wave
        
        Debug.Log($"Wave {waveNumber} started! Spawning {currentEnemies} enemies.");

        for (int i = 0; i < currentEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f); // Delay between spawns
        }
        
        waveNumber++; // Increment wave after completion
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().waveManager = this; // Reference for tracking
    }

    public void EnemyDefeated()
    {
        currentEnemies--;

        if (currentEnemies <= 0 && isWaveActive)
        {
            Debug.Log("All enemies defeated! Returning to preparation phase.");
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over! Restart or Exit.");
        currentState = GameState.GameOver;
    }
}
