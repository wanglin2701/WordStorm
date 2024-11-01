using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class BossManager : MonoBehaviour
{
    public int ID;
    public int waveNo;
    public int bossHealth = 35;
    public int bossLives = 7;
    private float bossTimer = 15f;  // Timer for each prefix challenge
    public TextMeshProUGUI bossTimerText;
    public Slider healthBar; // Reference to the Slider component

    public Sprite[] damageSprites; // Array to store the sliced sprites
    private int currentDamageSpriteIndex = 0;
    public SpriteRenderer bossSpriteRenderer; // Link this to the boss's SpriteRenderer

    public GameObject[] enemyPrefabs; // Prefabs for spawning prefix enemies
    public Transform spawnPoint; // Spawn point for enemies

    private int enemiesKilled = 0;
    private int nextKillThreshold = 3;
    private PlayerHealth playerHealth;
    private GameObject currentEnemy; // Track the current active enemy

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        healthBar.maxValue = bossHealth;
        healthBar.value = bossHealth;
        StartCoroutine(BossFightTimer());
    }

    void Update()
    {
        if (bossHealth <= 0 || bossLives <= 0)
        {
            Debug.Log("Boss defeated!");
            StopCoroutine(BossFightTimer());
            EndBossFight();
        }
    }

    private IEnumerator BossFightTimer()
    {
        while (bossHealth > 0 && bossLives > 0)
        {
            bossTimer = 15f;
            
            // Countdown loop for the 15-second timer
            while (bossTimer > 0 && currentEnemy != null)
            {
                bossTimer -= Time.deltaTime;
                bossTimerText.text = $"Boss Timer: {bossTimer:F2}s";
                yield return null;
            }

            // If the enemy is still present after 15 seconds, the player takes damage
            if (currentEnemy != null && bossTimer <= 0)
            {
                Debug.Log("Time's up! Player takes damage.");
                playerHealth.TakeDamage();
                Destroy(currentEnemy); 
                currentEnemy = null;
            }

            if(currentEnemy == null)
            {
                SpawnPrefixEnemy();
            }
        }
    }

    public void RegisterEnemyKill()
    {
        enemiesKilled++;
        currentEnemy = null; // Enemy is defeated, ready to spawn a new one

        if (enemiesKilled >= nextKillThreshold)
        {
            enemiesKilled = 0;
            bossLives--;
            nextKillThreshold = bossLives > 4 ? 3 : 4;
            ApplyDamageToBoss(1);
        }

        //ResetTimer(); // Immediately reset timer and spawn a new enemy
    }
    private void SpawnPrefixEnemy()
    {
        Debug.Log($"Attempting to spawn 15 enemies in total for Boss Wave {waveNo}.");
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("Enemy prefabs array is empty or not assigned.");
            return;  // Exit the method to avoid further errors
        }
        
        if (currentEnemy == null)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            currentEnemy = Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

            EnemyAI enemyAI = currentEnemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.isBossWave = true; // Disable movement for this enemy
            }
        }
    }

    //should be one enemy spawn per 15 secs, player type in full word, damage dealt to boss, if not player lose one live (one heart) after 15 secs.
    //Enemy should be fixed in place after being summoned. 

    public void ApplyDamageToBoss(int damage)
    {
        bossHealth -= damage;
        if (healthBar != null) healthBar.value = bossHealth;
        Debug.Log($"Boss Health: {bossHealth}");

        UpdateBossSprite();

        if (bossHealth <= 0)
        {
            EndBossFight();
        }
    }

    private void UpdateBossSprite()
    {
        if (bossSpriteRenderer != null && damageSprites.Length > 0)
        {
            currentDamageSpriteIndex = Mathf.Min(currentDamageSpriteIndex + 1, damageSprites.Length - 1);
            bossSpriteRenderer.sprite = damageSprites[currentDamageSpriteIndex];
        }
    }
    
    public void StartBossFight()
    {
        bossTimer = 15f; // Reset the timer to 15 seconds
        //ResetTimer();
        UpdateBossUI(true); // Method to update the visibility or state of boss-related UI elements
        // Start the coroutine only if this GameObject is active in hierarchy
        StartCoroutine(BossFightTimer());
    }

    private void EndBossFight()
    {
        StopCoroutine(BossFightTimer());  // Stop the boss fight timer
        bossTimerText.gameObject.SetActive(false);  // Hide the boss timer UI
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(false);  // Hide the health bar
        }

        Debug.Log("Boss defeated!");
    }

    private void UpdateBossUI(bool show)
    {
        bossTimerText.gameObject.SetActive(show);
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(show);  // Control the visibility of the health bar
        }
    }
}
