using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using EmeraldAI;
using EmeraldAI.Utility;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager WMmanager;

    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemyPrefabs; // Array of enemy prefabs for this wave
    }

    public GameObject[] enemies; // Array of all enemy prefabs
    public float timeBetweenWaves = 10f; // Time between each wave
    public float initialDelay = 3f; // Initial delay before the first wave starts
    public float timeBetweenEnemies = 1f; // Time between spawning each enemy in a wave
    public int maxActiveEnemies = 2; // Maximum number of active enemies at a time
    public int MaxActive = 5;
    private int currentLevel = 1;
    public int currentWaveIndex = 0;
    public int MaxWaveIndex = 0;

    public int currentActiveEnemies = 0;
    private float nextWaveTime;
    private bool isWaveInProgress = false;
    public GameObject[] SpawnPoints;
    public int randomWave = 0;
    public List<GameObject> ActiveEnemies;

    public Wave WavePerLevel;
    public int enemiesPerWave;

    private int totalEnemiesInLevel;

    private void Start()
    {
        if (WMmanager == null)
        {
            WMmanager = this;
        }
        nextWaveTime = Time.time + initialDelay;
        currentLevel = PlayerPrefs.GetInt("LevelNo", 1);

        WavePerLevel = CreateWave();
        enemiesPerWave = WavePerLevel.enemyPrefabs.Length;

        // Calculate total enemies in the level
        totalEnemiesInLevel = CalculateTotalEnemiesInLevel();

        UpdateEnemiesLeftUI();

        StartNextWave();
    }

    private void Update()
    {
        if (!isWaveInProgress && Time.time > nextWaveTime)
        {
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < MaxWaveIndex)
        {
            Debug.Log("Creating Wave " + currentWaveIndex);
            StartCoroutine(SpawnWave(WavePerLevel));
            currentWaveIndex++;
            nextWaveTime = Time.time + timeBetweenWaves;
        }
        else if (currentActiveEnemies <= 0)
        {
            // Level completed only when all enemies are defeated
            StartCoroutine(GameManager.instance.LevelComplete());
        }
    }

    private Wave CreateWave()
    {
        Wave wave = new Wave();
        if (currentLevel <= enemies.Length)
        {
            Debug.Log("Current Level < Enemies: " + currentLevel + " " + enemies.Length);
            wave.enemyPrefabs = new GameObject[currentLevel];
            for (int i = 0; i < currentLevel; i++)
            {
                wave.enemyPrefabs[i] = enemies[i];
            }
            maxActiveEnemies = currentLevel;
            if (maxActiveEnemies >= MaxActive)
            {
                maxActiveEnemies = MaxActive;
            }
            MaxWaveIndex = currentLevel;
        }
        else
        {
            Debug.Log("Random Wave");

            if (randomWave == 0)
            {
                randomWave = Random.Range(1, enemies.Length);
            }
            wave.enemyPrefabs = new GameObject[randomWave];
            for (int i = 0; i < randomWave; i++)
            {
                wave.enemyPrefabs[i] = enemies[i];
            }
            maxActiveEnemies = currentLevel;
            if (maxActiveEnemies >= MaxActive)
            {
                maxActiveEnemies = MaxActive;
            }
            MaxWaveIndex = currentLevel;
        }

        return wave;
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        isWaveInProgress = true;
        foreach (GameObject enemyPrefab in wave.enemyPrefabs)
        {
            if (currentActiveEnemies < maxActiveEnemies)
            {
                SpawnEnemy(enemyPrefab);
                currentActiveEnemies++;
            }
            else
            {
                yield return new WaitUntil(() => currentActiveEnemies < maxActiveEnemies);
                SpawnEnemy(enemyPrefab);
                currentActiveEnemies++;
            }
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        isWaveInProgress = false;
        Debug.Log("New Wave Coming");
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
        ActiveEnemies.Add(enemy);
        enemy.GetComponent<EmeraldAISystem>().OnDeath += ()=> { HandleEnemyDeath(enemy.GetComponent<EnemyDeathCoins>().coinsOnDeath); };
    }

    public void HandleEnemyDeath(int coinsOnDeath)
    {
        currentActiveEnemies--;
        GameManager.instance.EnemiesDefeated += 1;

        UpdateEnemiesLeftUI();

        if (currentWaveIndex >= MaxWaveIndex && currentActiveEnemies <= 0)
        {
            StartCoroutine(GameManager.instance.LevelComplete());
        }

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coinsOnDeath);
        GameManager.instance.UpdateCoinsUi();
    }

    private void UpdateEnemiesLeftUI()
    {
        int enemiesLeft = totalEnemiesInLevel - GameManager.instance.EnemiesDefeated;
        GameManager.instance.EnemiesDefeatedGamePlay.text = enemiesLeft.ToString();
    }

    private int CalculateTotalEnemiesInLevel()
    {
        // Calculate total enemies by summing all waves
        int totalEnemies = 0;
        for (int i = 0; i < MaxWaveIndex; i++)
        {
            totalEnemies += WavePerLevel.enemyPrefabs.Length;
        }
        return totalEnemies;
    }

    public void RemoveEnemy(GameObject EnemyGO)
    {
        ActiveEnemies.Remove(EnemyGO);
    }
}
