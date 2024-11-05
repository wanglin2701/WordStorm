using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class BossManager : MonoBehaviour
{
    public int ID;
    public int waveNo;
    public int bossLives = 7;
    public float bossTimer = 15f;  // Timer for each prefix challenge
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

    public GameObject BossDamage_Particles;
    public GameObject BossDeath_Particles;
    public GameObject BossBlackSmoke;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        bossController = GetComponent<Animator>();

        healthBar.maxValue = bossLives;
        healthBar.value = bossLives;
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

    private IEnumerator BossFightTimer()
    {
        while (bossLives > 0 && playerHealth != null && playerHealth.currentHealth > 0)
        {
            bossTimer = 15f;

            // Countdown loop for the 15-second timer
            while (bossTimer > 0 && currentEnemy != null && playerHealth.currentHealth > 0)
            {
                bossTimer -= Time.deltaTime;
                bossTimerText.text = $"{bossTimer:F2}s";
                yield return null;
            }

            // If the timer runs out and the enemy is still present, the player loses a life
            if (currentEnemy != null && bossTimer <= 0 && playerHealth.currentHealth > 0)
            {
                SoundManager.instance.PlaySound("BossShoot");

                Debug.Log("Time's up! Player takes damage.");
                playerHealth.TakeDamage();

                // Destroy the current enemy and reset
                Destroy(currentEnemy);
                currentEnemy = null;

                yield return new WaitForSeconds(1f);
            }

            // Ensure only one enemy is active at any time
            if (playerHealth.currentHealth <= 0 && bossLives <= 0)
            {
                EndBossFight();
                yield break;
            }

            if (currentEnemy == null && bossLives > 0)
            {
                SpawnPrefixEnemy();
                bossTimer = 15f;
            }
        }
    }

    public void RegisterEnemyKill()
    {
        if (currentEnemy != null && currentEnemy.GetComponent<EnemyAI>().isBossWave)
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

                    healthBar.value = bossLives;

                    
                    

                    UpdateBossSprite();



                    if (bossLives <= 0)
                    {
                        Instantiate(BossDeath_Particles, transform.position, Quaternion.identity);
                        Instantiate(BossBlackSmoke, transform.position, Quaternion.identity);

                        SoundManager.instance.PlaySound("BossDie");
                        bossController.SetBool("isDead", true);
                        EndBossFight();
                    }

                    else
                    {
                        SoundManager.instance.PlaySound("BossDamage");
                        Instantiate(BossDamage_Particles, transform.position, Quaternion.identity);

                        bossController.SetBool("isIdle", false);
                        bossController.SetBool("takeDamage", true);
                    }
                }
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
        if (bossLives > 0)
        {
            


            bossLives = Mathf.Max(bossLives - damage, 0);  // Ensure bossHealth does not go negative

      

            //if (healthBar != null) healthBar.value = bossLives;
            Debug.Log($"Boss Health: {bossLives}");

            UpdateBossSprite();

            if (bossLives <= 0)
            { 
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
        bossTimer = 15f; // Reset the timer to 15 seconds
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
        yield return new WaitForSeconds(1.0f);
        SceneHandler.LoadClearedLevel1();


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
