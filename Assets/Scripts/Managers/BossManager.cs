using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class BossManager : MonoBehaviour
{
    public int ID;
    public int waveNo;
    public int bossHealth = 7;
    public int bossLives = 25;
    private float bossTimer = 35f;  // Timer for each prefix challenge
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

    public Animator bossController;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        bossController = GetComponent<Animator>();

        healthBar.maxValue = bossHealth;
        healthBar.value = bossHealth;
        StartCoroutine(BossFightTimer());
    }

    private void FixedUpdate()
    {
        if (bossController != null && bossController.gameObject != null)
        {
            if (bossController.GetCurrentAnimatorStateInfo(0).IsName("TakeDamage") && bossController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                bossController.SetBool("isIdle", true);
                bossController.SetBool("takeDamage", false);
            }

            if (bossController.GetCurrentAnimatorStateInfo(0).IsName("Dying") && bossController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                bossController.SetBool("dying", false);
            }
        }
    }

    void Update()
    {
        if (bossHealth <= 0 || bossLives <= 0)
        {

            Debug.Log("Boss defeated!");
            EndBossFight();
        }
    }

    private IEnumerator BossFightTimer()
    {
        while (bossHealth > 0 && bossLives > 0)
        {
            bossTimer = 35f;

            // Countdown loop for the 25-second timer
            while (bossTimer > 0 && currentEnemy != null)
            {
                bossTimer -= Time.deltaTime;
                bossTimerText.text = $"Boss Timer: {bossTimer:F2}s";
                yield return null;
            }

            // If the timer runs out and the enemy is still present, the player loses a life
            if (currentEnemy != null && bossTimer <= 0)
            {
                Debug.Log("Time's up! Player takes damage.");
                playerHealth.TakeDamage();

                // Destroy the current enemy and reset
                Destroy(currentEnemy);
                currentEnemy = null;

                yield return new WaitForSeconds(1f);
            }

            // Ensure only one enemy is active at any time
            if (currentEnemy == null && bossLives > 0)
            {
                SpawnPrefixEnemy();
                bossTimer = 35f;
            }
        }
    }

    public void RegisterEnemyKill()
    {
        enemiesKilled++;
        
        if (currentEnemy != null)
        {
            Destroy(currentEnemy);  // Destroy the current enemy to clean up
            currentEnemy = null;
        }

        if (enemiesKilled >= nextKillThreshold)
        {
            enemiesKilled = 0;
            if (bossLives > 0)
            {
                bossLives--;
                nextKillThreshold = bossLives >= 4 ? 3 : 4;
                ApplyDamageToBoss(1);
            }
        }
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

    public void ApplyDamageToBoss(int damage)
    {
        if (bossHealth > 0)
        {
            bossHealth = Mathf.Max(bossHealth - damage, 0);  // Ensure bossHealth does not go negative

            bossController.SetBool("isIdle", false);
            bossController.SetBool("takeDamage", true);


            if (healthBar != null) healthBar.value = bossHealth;
            Debug.Log($"Boss Health: {bossHealth}");

            UpdateBossSprite();

            if (bossHealth <= 0)
            { 
                bossController.SetBool("Dying", true);
                EndBossFight();
            }
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
        bossTimer = 35f; // Reset the timer to 25 seconds
        UpdateBossUI(true); // Method to update the visibility or state of boss-related UI elements

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

        StartCoroutine(WaitForDeathAnimation());

        Debug.Log("Boss defeated!");
    }


    private IEnumerator WaitForDeathAnimation()
    {
        // Ensure the boss 'Dying' animation is being played
        while (bossController.GetCurrentAnimatorStateInfo(0).IsName("Dying") && bossController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null; // Wait until the frame where the animation has completed
        }
        
        // Wait for a few seconds before transitioning to the start screen
        yield return new WaitForSeconds(5.0f);

        SceneHandler.LoadStartScreen(); 
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
