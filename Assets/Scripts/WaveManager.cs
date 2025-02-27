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
    public GameObject enemyPrefab3;
    public GameObject UIControls; // Reference for the UI Controls
    public GameObject upgradeMenu; // Reference for the UpgradeMenu
    public GarageDoorController garageDoor; // Add this reference in the Inspector
    public int baseEnemyCount = 5;

    [Header("Debug Info")]
    public int currentEnemiesAlive;

    private int highScore; // Store the high score
    private bool isWaveActive;
    private HashSet<GameObject> activeEnemies = new HashSet<GameObject>(); // Track active enemies

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Load the saved high score
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
        upgradeMenu.SetActive(false); // Ensure UpgradeMenu is toggled off
        isWaveActive = true;
        activeEnemies.Clear();
        int enemiesToSpawn = baseEnemyCount + (waveNumber * 2);
        currentEnemiesAlive = enemiesToSpawn;

        // Open the garage door at the start of the wave
        garageDoor.OpenDoor();

        Debug.Log($"Wave {waveNumber} started! Spawning {enemiesToSpawn} enemies.");

        // Start the wave text display
        FindObjectOfType<WaveText>().StartWaveTextDisplay(waveNumber);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.2f);
        }

        waveNumber++;
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyToSpawn;

        float randomValue = Random.value; // Random number between 0 and 1

        if (waveNumber > 5)
        {
            // 50% chance for enemy1, 40% for enemy2, 10% for enemy3
            if (randomValue <= 0.50f)
            {
                enemyToSpawn = enemyPrefab;
            }
            else if (randomValue <= 0.90f)
            {
                enemyToSpawn = enemyPrefab2;
            }
            else
            {
                enemyToSpawn = enemyPrefab3;
            }
        }
        else if (waveNumber > 3)
        {
            // 2/3 for enemy1, 1/3 for enemy2
            enemyToSpawn = (randomValue <= 0.6667f) ? enemyPrefab : enemyPrefab2;
        }
        else
        {
            enemyToSpawn = enemyPrefab;
        }

        // Instantiate enemy
        GameObject enemyObject = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
        activeEnemies.Add(enemyObject); // Add enemy to tracking list

        // Assign the correct EnemyType
        Enemy enemyScript = enemyObject.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.waveManager = this;

            // Assign the enemy type based on which prefab was used
            if (enemyToSpawn == enemyPrefab)
            {
                enemyScript.enemyType = EnemyType.Regular;
            }
            else if (enemyToSpawn == enemyPrefab2)
            {
                enemyScript.enemyType = EnemyType.Strong;
            }
            else if (enemyToSpawn == enemyPrefab3)
            {
                enemyScript.enemyType = EnemyType.Boss; // Assign Enemy3 as "Boss"
            }
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
        Debug.Log("GameOver() method called!");

        currentState = GameState.GameOver;

        // Check for a new high score
        if (waveNumber - 1 > highScore)
        {
            highScore = waveNumber - 1;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            Debug.Log("New High Score Saved: " + highScore);
        }
    }
}
