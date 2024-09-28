using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array to hold different enemy prefabs (pro, com, dis, anti)
    public Transform[] spawnPoints;   // Array of spawn points for enemies
    public float spawnRate = 2f;      // Rate of spawning enemies in seconds
    private int waveNumber = 0;        // Start at wave 1
    public TextMeshProUGUI timerText; // Timer display for the UI

    private float levelStartTime;     // Time when the level starts
    private bool levelActive = true;  // Flag to check if the level is still active
    
    void Start()
    {
        levelStartTime = Time.time;  // Record the start time of the level
        StartCoroutine(SpawnWave());
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator SpawnWave()
    {
        while (waveNumber < 3) // Assuming there are 3 waves before the boss
        {
            int[] enemiesTypeCount = GetEnemiesCountByWave(waveNumber);
            
            for (int i = 0; i < enemiesTypeCount[0]; i++)
            {
                SpawnEnemy(Random.Range(0, 3));  // Spawns one of the Normal Minion prefabs
                yield return new WaitForSeconds(spawnRate);
            }
            for (int j = 0; j < enemiesTypeCount[1]; j++)
            {
                SpawnEnemy(3);  // Always spawns the Armored Minion prefab
                yield return new WaitForSeconds(spawnRate);
            }

            // Wait until all enemies from the current wave are cleared
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            waveNumber++;
            if (waveNumber < 3)
            {
                continue;  // Proceed to next iteration without delay
            }
        }
        
        levelActive = false;
        StartBossLevel();
    }

    private IEnumerator UpdateTimer()
    {
        while (levelActive)
        {
            float elapsedTime = Time.time - levelStartTime;
            string minutes = ((int) elapsedTime / 60).ToString("00");
            string seconds = (elapsedTime % 60).ToString("00");
            timerText.text = $"{minutes}:{seconds}";
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void SpawnEnemy(int type)
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefabs[type], randomSpawnPoint.position, Quaternion.identity);
    }

    private void StartBossLevel()
    {
        Debug.Log("Starting Boss Level");
        // Initialize boss fight with specific settings or spawn boss
    }
    
    private int[] GetEnemiesCountByWave(int wave)
    {
        switch (wave)
        {
            case 0: return new int[]{4, 0};
            case 1: return new int[]{5, 2};
            case 2: return new int[]{6, 4};
            default: return new int[]{0, 0};
        }
    }
}
