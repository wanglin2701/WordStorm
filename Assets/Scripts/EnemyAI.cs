using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public int health = 1;

    public string enemyPrefix; // Prefix linked to this enemy

    private static int currentEnemies = 0; // Number of enemies currently alive
    public static int enemiesPerWave = 4; // Number of enemies per wave
    public static Transform[] spawnPoints; // Array of spawn points

    private static PrefixDictionary prefixDictionary; // Word dictionary for prefixes


    void Start()
    {
        // Ensure player reference and word dictionary are set
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (prefixDictionary == null)
        {
            prefixDictionary = FindObjectOfType<PrefixDictionary>();
        }

        // Set spawn points if they haven't been set yet
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            spawnPoints = GameObject.FindWithTag("SpawnPoint").GetComponentsInChildren<Transform>();
        }
        
        // If there are no active enemies, spawn the next wave
        if (currentEnemies == 0)
        {
            SpawnWave();
        }
    }
    
    void Update()
    {
        // Calculate direction towards the player
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Calculate the angle to rotate towards the player
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Move the enemy towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        // Rotate the enemy to face the player
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    // Spawns a wave of enemies
    public static void SpawnWave()
    {
        currentEnemies = enemiesPerWave; // Reset enemy count for the wave

        for (int i = 0; i < enemiesPerWave; i++)
        {
        //    Choose a random spawn point from the array
        //    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        //    Choose a random prefix for the enemy
        //    List<string> keys = new List<string>(prefixDictionary.wordDictionary.Keys);
        //    string randomPrefix = keys[Random.Range(0, keys.Count)];

        //    Instantiate a new enemy at the selected spawn point
        //    GameObject enemy = Instantiate(Resources.Load<GameObject>("Enemy pro"), spawnPoint.position, spawnPoint.rotation);
        
        //    Assign the prefix to the new enemy
        //    EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
        //    enemyAI.enemyPrefix = randomPrefix;

        }
    }

    
    // Call this method to apply damage to the enemy
    public void TakeDamage(int damage)
    {
        health -= damage;

        // If health is 0 or less, destroy the enemy
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    
    // Call this when an enemy is destroyed
    public void DestroyEnemy()
    {
        currentEnemies--;

        // Destroy the enemy game object
        Destroy(gameObject);

        // If all enemies are destroyed, spawn the next wave
        if (currentEnemies == 0)
        {
            SpawnWave();
        }
    }

    // This method checks if the player's input matches any active enemy prefix
    public static string CheckInput(string playerInput)
    {
        EnemyAI[] allEnemies = FindObjectsOfType<EnemyAI>();

        foreach (EnemyAI enemy in allEnemies)
        {
            // Get the list of valid words for the enemy's prefix
            if (prefixDictionary.wordDictionary.ContainsKey(enemy.enemyPrefix))
            {
                List<string> validWords = prefixDictionary.wordDictionary[enemy.enemyPrefix];
                
                // Check if the player's input matches any valid word exactly
                if (validWords.Contains(playerInput))
                {
                      return enemy.enemyPrefix; // Return the matching prefix
                }
            }
        }
        return null; // Return null if no match is found

    }
}
