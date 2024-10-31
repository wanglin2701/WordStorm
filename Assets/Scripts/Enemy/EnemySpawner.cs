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
    private int waveNumber = 1;        // Start at wave 1
    public TextMeshProUGUI timerText; // Timer display for the UI

    private float levelStartTime;     // Time when the level starts
    private bool levelActive = true;  // Flag to check if the level is still active

    public TextMeshProUGUI LevelInfo;
    public GameObject bossGameObject; // Reference to the boss GameObject
    
    void Start()
    {
        LevelInfo.text = "Level 1 | Wave " + waveNumber;
        levelStartTime = Time.time;  // Record the start time of the level
        StartCoroutine(SpawnWave());
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator SpawnWave()
    {
        EnemyWaveList waves = GameData.GetEnemyWaveList();
        while (waveNumber <= waves.EnemyWave.Length)
        {
            if (waveNumber == 4) // Boss wave check
            {
                StartBossLevel();
                yield break;  // Ends coroutine to stop further spawning
            }

            EnemyWave currentWave = waves.EnemyWave[waveNumber - 1];
            string[] enemyIDs = currentWave.enemyID.Split(',');

            foreach (string enemyID in enemyIDs)
            {
                if (int.TryParse(enemyID.Trim(), out int enemyIndex))
                {
                    for (int i = 0; i < currentWave.enemyCount; i++)
                    {
                        SpawnEnemy(enemyIndex);
                        yield return new WaitForSeconds(currentWave.spawnRate);
                    }
                }
            }

            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            waveNumber++;
            LevelInfo.text = "Level 1 | Wave " + waveNumber;
        }
    }

    private IEnumerator UpdateTimer()
    {
        while (levelActive)
        {
            float elapsedTime = Time.time - levelStartTime;
            string minutes = ((int)elapsedTime / 60).ToString("00");
            string seconds = (elapsedTime % 60).ToString("00");
            timerText.text = $"{minutes}:{seconds}";
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void SpawnEnemy(int type)
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        foreach (GameObject enemy in enemyPrefabs)
        {
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            
            if (enemyAI != null && enemyAI.ID == type)
            {
                Instantiate(enemy, randomSpawnPoint.position, Quaternion.identity);
                return;
            }
        }

        Debug.LogError($"Enemy ID {type} not found in enemyPrefabs array.");
    }

    private void StartBossLevel()
    {
                // // Show the popup panel
                // if (bossGameObject != null)
                // {
                //     bossGameObject.SetActive(true);
                // }

                // // Wait for a brief moment with the popup then start the boss level
                // StartCoroutine(StartBossFightAfterDelay(2)); // 2 seconds for reading the popup

         if (bossGameObject != null)
        {
            bossGameObject.SetActive(true);
            BossManager bossManager = bossGameObject.GetComponent<BossManager>();
            if (bossManager != null && bossGameObject.activeInHierarchy)
            {
                bossManager.StartBossFight();
                Debug.Log("Boss Fight Started");
            }
        }
        else
        {
            Debug.LogError("Failed to start Boss Fight: Boss GameObject not active.");
        }        
    }

    private IEnumerator StartBossFightAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (bossGameObject != null)
        {
            bossGameObject.SetActive(false);
        }
        if (bossGameObject != null)
        {
            bossGameObject.SetActive(true); // Activate the boss
            Debug.Log("Boss GameObject Activated");

            yield return null;  // Wait for one frame to ensure the GameObject is fully activated
            Debug.Log($"Boss GameObject active status post-wait: {bossGameObject.activeSelf}");

            // Now that we've waited a frame after activation, retrieve and start the boss fight
            BossManager bossManager = bossGameObject.GetComponent<BossManager>();
            if (bossManager != null && bossGameObject.activeInHierarchy)
            {
                bossManager.StartBossFight(); // Now start the fight which will start the coroutine
                Debug.Log("Boss Fight Started");
            }
            }
            else
            {
                Debug.LogError("Failed to start Boss Fight: GameObject is not active in hierarchy.");
            }
    }
}

// private IEnumerator SpawnWave()
    // {
    //     EnemyWaveList waves = GameData.GetEnemyWaveList();
    //     while (waveNumber <= waves.EnemyWave.Length)
    //     {
    //         if (waveNumber == 4)
    //         {
    //             StartBossLevel();
    //             yield break;  // Stop spawning other waves
    //         }
            
    //         foreach (EnemyWave wave in waves.EnemyWave)
    //         {
    //             string[] enemyIDs = wave.enemyID.Split(',');
    //             foreach (string enemyID in enemyIDs)
    //             {
    //                 if (int.TryParse(enemyID.Trim(), out int enemyIndex))
    //                 {
    //                     print($"Enemy Index: {enemyIndex}");

    //                     if (wave.enemyCount > 0)
    //                     {
    //                         for (int i = 0; i < wave.enemyCount; i++)
    //                         {
    //                             SpawnEnemy(enemyIndex);
    //                             yield return new WaitForSeconds(wave.spawnRate);
    //                         }
    //                     }
    //                     else
    //                     {
    //                         Debug.LogError($"Wave's Enemy count is 0 or less. Check your JSON data for enemy IDs.");
    //                     }
    //                 }

    //                 yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

    //                 waveNumber++;
    //                 LevelInfo.text = "Level 1 | Wave " + waveNumber;

    //                 if (waveNumber > waves.EnemyWave.Length)
    //                 {
    //                     levelActive = false;
    //                     break;
    //                 }
                    
    //             }

    //     StartBossLevel();
    //         }
    //     }
    // }
