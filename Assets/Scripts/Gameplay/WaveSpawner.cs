
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_nextWaveText;
    [SerializeField] private float countdown;
    [SerializeField] Transform[] spawnPositions;
    public Wave[] waves;
    

    public int currentWaveIndex = 0;

    private bool readyToCountDown;
    List<int> usedSpawnIndexes = new List<int>();
    private void Start()
    {
        readyToCountDown = true;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
        
    }

    public void ResetWaves()
    {
        readyToCountDown = true;
        currentWaveIndex = 0;
        usedSpawnIndexes.Clear();
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
        
    }
    private void Update()
    {
        if (currentWaveIndex >= waves.Length && !GameManager.instance.isGameOver)
        {
            GameManager.instance.LevelComplete();            
            //ResetWaves();
            //Debug.Log("You survived every wave!");
            return;
        }

        if (readyToCountDown == true)
        {
            usedSpawnIndexes.Clear();
            countdown -= Time.deltaTime;
        }

        if (countdown > 1 && countdown < waves[currentWaveIndex].timeToNextWave)
        {
            Debug.LogError(Convert.ToInt32(countdown));
            m_nextWaveText.transform.parent.gameObject.SetActive(true);
            m_nextWaveText.text = Convert.ToInt32(countdown).ToString();
        }
        else 
        {
            m_nextWaveText.transform.parent.gameObject.SetActive(false);
        }
        if (countdown <= 0)
        {
            readyToCountDown = false;

            countdown = waves[currentWaveIndex].timeToNextWave;

            StartCoroutine(SpawnWave());
        }

        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true;

            currentWaveIndex++;
        }
    }
    private IEnumerator SpawnWave()
    {
        
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
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
                //Debug.LogError(randomIndex);
                Transform spawnPoint = spawnPositions[randomIndex];
                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }
}

[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}
