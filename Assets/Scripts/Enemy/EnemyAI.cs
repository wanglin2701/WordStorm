using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float stoppingDistance = 1f; // The minimum distance between the enemy and the player
    public float enemyAvoidanceDistance = 0.8f; // The minimum distance between enemies
    public float separationForce = 1.5f; // The force applied to steer away from other enemies
    public int health = 1;
    public string enemyPrefix; // Prefix linked to this enemy
    private static int currentEnemies = 0; // Number of enemies currently alive
    public static int enemiesPerWave = 4; // Number of enemies per wave
    public static Transform[] spawnPoints; // Array of spawn points

    //private static PrefixDictionary prefixDictionary; // Word dictionary for prefixes
    private static Dictionary<string, List<string>> WSDictionary;

    private static Dictionary<string, int> usedWords = new Dictionary<string, int>();

    private bool hasDamagedPlayer = false; // Flag to ensure health is only decreased once

    private void Start()
    {
        //Get Dictionary data
        WSDictionary = GameData.GetWordStormDictionary();

        // Ensure player reference and word dictionary are set
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        //if (prefixDictionary == null)
        //{
        //    prefixDictionary = FindObjectOfType<PrefixDictionary>();
        //}

        // Set spawn points if they haven't been set yet
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            spawnPoints = GameObject.FindWithTag("SpawnPoint").GetComponentsInChildren<Transform>();
        }
    }
    
    private void Update()
    {
        // Calculate direction towards the player
        //Vector2 direction = player.transform.position - transform.position;
        //direction.Normalize();

        // Calculate the angle to rotate towards the player
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Move the enemy towards the player
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        // Rotate the enemy to face the player
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        // Calculate direction towards the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Apply separation behavior to avoid overlapping with other enemies
        Vector2 separation = GetSeparationForce();

        // Combine movement towards the player and separation to avoid other enemies
        Vector2 finalDirection = direction + separation;
        finalDirection.Normalize(); // Normalize to maintain the speed

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // If the enemy is further than the stopping distance, move toward the player
        if (distanceToPlayer > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)finalDirection, speed * Time.deltaTime);
        }
    }
    private Vector2 GetSeparationForce()
    {
        Vector2 separationForce = Vector2.zero;

        // Get all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject otherEnemy in enemies)
        {
            if (otherEnemy != this.gameObject) // Ignore itself
            {
                float distanceToEnemy = Vector2.Distance(transform.position, otherEnemy.transform.position);

                // If the enemy is too close, calculate the force to move away
                if (distanceToEnemy < enemyAvoidanceDistance)
                {
                    Vector2 directionAway = (transform.position - otherEnemy.transform.position).normalized;
                    separationForce += directionAway * (enemyAvoidanceDistance - distanceToEnemy) * separationForce;
                }
            }
        }

        return separationForce;
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
    }

    // This method checks if the player's input matches any active enemy prefix
    public static string CheckInput(string playerInput)
    {
        EnemyAI[] allEnemies = FindObjectsOfType<EnemyAI>();

        foreach (EnemyAI enemy in allEnemies)
        {
            if (WSDictionary.ContainsKey(enemy.enemyPrefix))
            {
                List<string> validWords = WSDictionary[enemy.enemyPrefix];

                // Check if the player's input matches any valid word exactly
                if (validWords.Contains(playerInput))
                {
                    
                    if (usedWords.ContainsKey(playerInput))
                    {
                        if (usedWords[playerInput] >= 2)  //For testing purpose, each word can only use twice
                        {
                            return null;  //Return null so player cannot reuse words
                        }

                        else
                        {
                            usedWords[playerInput]++;

                        }
                    }

                    else
                    {
                        usedWords.Add(playerInput, +1);

                    }

                    //Debug.Log(playerInput);
                    //Debug.Log(usedWords[playerInput]);

                    return enemy.enemyPrefix; // Return the matching prefix
                }
            }


            //// Get the list of valid words for the enemy's prefix
            //if (prefixDictionary.wordDictionary.ContainsKey(enemy.enemyPrefix))
            //{
            //    List<string> validWords = prefixDictionary.wordDictionary[enemy.enemyPrefix];

            //    // Check if the player's input matches any valid word exactly
            //    if (validWords.Contains(playerInput))
            //    {
            //        return enemy.enemyPrefix; // Return the matching prefix
            //    }
            //}
        }
        return null; // Return null if no match is found
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasDamagedPlayer)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
                hasDamagedPlayer = true; // Ensure this enemy only damages the player once
            }
        }
    }
}
