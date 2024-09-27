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
    private float timeBetweenWaves = 5f; // Time delay between enemy spawns
    private float levelTimer = 59f;      // Timer for boss fight
    private bool isBossLevel = false;
    public TextMeshProUGUI timerText;
    
    void Start()
    {
        StartCoroutine(LevelTimer());
        StartCoroutine(SpawnWave());
    }

    
    private IEnumerator SpawnWave()
    {
         while (true)
        {
            int[] enemiesTypeCount;
            if (waveNumber == 0) { enemiesTypeCount = new int[]{4, 0}; }
            else if (waveNumber == 1) { enemiesTypeCount = new int[]{5, 2}; }
            else if (waveNumber == 2) { enemiesTypeCount = new int[]{6, 4}; }
            else { enemiesTypeCount = new int[]{0, 0}; isBossLevel = true; }

            for (int i = 0; i < enemiesTypeCount[0]; i++)
            {
                SpawnEnemy(Random.Range(0, 3)); // Spawns one of the Normal Minion prefabs
                yield return new WaitForSeconds(spawnRate);
            }
            for (int j = 0; j < enemiesTypeCount[1]; j++)
            {
                SpawnEnemy(3); // Always spawns the Armored Minion prefab
                yield return new WaitForSeconds(spawnRate);
            }

            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
            if (isBossLevel)
            {
                StartBossLevel();
                yield break;
            }

            waveNumber++;
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private void SpawnEnemy(int type)
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject spawnedEnemy = Instantiate(enemyPrefabs[type], randomSpawnPoint.position, Quaternion.identity);
        //Debug.Log("Spawned: " + spawnedEnemy.name + " at " + randomSpawnPoint.position);
    }

    private IEnumerator LevelTimer()
    {
        while (levelTimer > 0)
        {
            timerText.text = levelTimer.ToString();  // Update the text to show the remaining time
            yield return new WaitForSeconds(1f);
            levelTimer--;
            if (levelTimer == 0 && isBossLevel)
            {
                Debug.Log("Boss fight time is over!");
                timerText.text = "Time's Up!";
                // Handle end of boss fight (victory or failure)
            }
        }
    }

    private void StartBossLevel()
    {
        // Initialize boss fight with specific settings or spawn boss
        Debug.Log("Boss level started with a timer of 60 seconds.");
    }
}
