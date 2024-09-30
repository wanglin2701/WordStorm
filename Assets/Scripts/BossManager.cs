using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class BossManager : MonoBehaviour
{
    public int bossHealth = 50;
    private float bossTimer = 60f;
    public TextMeshProUGUI bossTimerText;
    public Slider healthBar; // Reference to the Slider component

    void Start()
    {
        healthBar.maxValue = bossHealth;
        healthBar.value = bossHealth;
        //StartCoroutine(BossFightTimer());
    }

    void Update()
    {
        // Update is typically used for constant checks or UI updates
        if (bossHealth <= 0)
        {
            Debug.Log("Boss defeated!");
            // Trigger victory condition, stop the timer
            StopCoroutine(BossFightTimer());
        }
    }
    private void UpdateBossUI(bool show)
    {
        bossTimerText.gameObject.SetActive(show);
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(show);  // Control the visibility of the health bar
        }
    }

    private IEnumerator BossFightTimer()
    {
        while (bossTimer > 0)
        {
            bossTimer -= Time.deltaTime;
            bossTimerText.text = $"Boss Timer: {bossTimer:F2}s";
            yield return null;

            if (bossTimer <= 0)
            {
                Debug.Log("Time's up! Player loses a life or the game.");
                EndBossFight();
                break;  // Exit the coroutine loop
            }
        }
    }

    public void ApplyDamageToBoss(int damage)
    {
        bossHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = bossHealth;  // Update the health bar UI
        }
        Debug.Log($"Boss Health: {bossHealth}");

        if (bossHealth <= 0)
        {
            EndBossFight();
        }
    }

    public void StartBossFight()
    {
        bossTimer = 60f; // Reset the timer to 60 seconds
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
        // Here can add any additional logic needed for when the boss fight ends
        // eg. could load a victory scene or display a win message
        // fore prefix, have to key in one word in 30 secs. 
    }
}
