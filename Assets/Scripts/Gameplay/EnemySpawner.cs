
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float spawnInterval = 3f; // Time interval between enemy spawns
    public int enemiesPerWave = 5; // Number of enemies per wave
    public int wavesCount = 3; // Total number of waves
    public float waveInterval = 10f; // Time interval between waves

    private int currentWave = 0; // Current wave count
    private int enemiesSpawned = 0; // Number of enemies spawned in the current wave
    private bool isSpawning = false; // Flag to check if spawning is in progress

    public Transform[] spawnPositions; // Array of spawn positions
    List<int> usedSpawnIndexes = new List<int>();

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWave++;
        enemiesSpawned = 0;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;

        Debug.LogError("enemiesSpawned: " + enemiesSpawned);

        while (enemiesSpawned < enemiesPerWave)
        {
            SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }

        // Check if all enemies from the current wave are killed
        while (GameObject.FindWithTag("enemy") != null)
        {
            yield return null; // Wait until all enemies are destroyed
        }

        yield return new WaitForSeconds(waveInterval);
        Debug.LogError("currentWave: " + currentWave);
        if (currentWave <= wavesCount)
        {
            StartNextWave();
        }
        else
        {
            Debug.Log("All waves completed!");
        }

        isSpawning = false;
    }

    

    void SpawnEnemy()
    {
        int randomIndex;

        // Keep trying to find an unused spawn position
        do
        {
            randomIndex = Random.Range(0, spawnPositions.Length);
        }
        while (usedSpawnIndexes.Contains(randomIndex));

        // Add the used spawn position index to the list
        usedSpawnIndexes.Add(randomIndex);

        // Get the spawn point from the array
        Transform spawnPoint = spawnPositions[randomIndex];

        // Instantiate the enemy with the same rotation as the spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}