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
    public GameObject popupPanel;     // Reference to the popup panel UI
    
    void Start()
    {
        LevelInfo.text = "Level 1 | Wave " + waveNumber;
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
            LevelInfo.text = "Level 1 | Wave " + waveNumber;

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
        // Show the popup panel
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }

        // Wait for a brief moment with the popup then start the boss level
        StartCoroutine(StartBossFightAfterDelay(2)); // 2 seconds for reading the popup
    }

    private IEnumerator StartBossFightAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
        if (bossGameObject != null)
        {
            bossGameObject.SetActive(true); // Activate the boss
            Debug.Log("Boss GameObject Activated");
            
            yield return null;  // Wait for one frame to ensure the GameObject is fully activated
            Debug.Log($"Boss GameObject active status post-wait: {bossGameObject.activeSelf}");
            
            // Now that we've waited a frame after activation, retrieve and start the boss fight
            //BossManager bossManager = bossGameObject.GetComponent<BossManager>();
            // if (bossManager != null && bossGameObject.activeInHierarchy)
            // {
            //     bossManager.StartBossFight(); // Now start the fight which will start the coroutine
            //     Debug.Log("Boss Fight Started");
            // }
            // }
            // else
            // {
            //     Debug.LogError("Failed to start Boss Fight: GameObject is not active in hierarchy.");
            }
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
