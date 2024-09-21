using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array to hold different enemy prefabs (pro, com, dis, anti)
    public Transform[] spawnPoints;   // Array of spawn points for enemies

    public int waveNumber = 1;        // Start at wave 1
    public float timeBetweenSpawns = 4f; // Time delay between enemy spawns
    public float spawnRate = 5f;    // Rate of spawning enemies
    private int enemiesToSpawn;       // Number of enemies to spawn in the current wave
    private int totalEnemiesInWave;   // Total enemies in the current wave
    void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        // Determine how many enemies to spawn based on the wave
        enemiesToSpawn = waveNumber * 3 + 1; // wave 1 spawns 4, wave 2 spawns 7
        totalEnemiesInWave = enemiesToSpawn;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        while (enemiesToSpawn > 0)
        {
            // Pick a random spawn point and random enemy type
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            
            // Instantiate enemy at the random spawn point
            GameObject spawnedEnemy = Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);

            // Debug message for testing
            Debug.Log("Spawned: " + spawnedEnemy.name + " at " + randomSpawnPoint.position);

            enemiesToSpawn--;

            // Wait before spawning the next enemy
            yield return new WaitForSeconds(spawnRate);
        }

        // After spawning, check if all enemies are destroyed to start the next wave
        StartCoroutine(CheckForNextWave());
    }

    private IEnumerator CheckForNextWave()
    {
        while (true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Check if any enemies are still active
            bool enemiesExist = false;
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null) // check if null before accessing
                {
                    enemiesExist = true;
                    break; // If any enemy is found, stop checking
                }
            }

            if (!enemiesExist)
            {
                // All enemies are destroyed, start the next wave
                waveNumber++;
                StartWave();
                yield break; // Exit the coroutine once the next wave starts
            }

            // Wait a second before checking again
            yield return new WaitForSeconds(1f);
        }
    }
}
