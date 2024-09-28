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

    void Start()
    {
        StartCoroutine(BossFightTimer());
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

    private IEnumerator BossFightTimer()
    {
        while (bossTimer > 0)
        {
            bossTimer -= Time.deltaTime;
            bossTimerText.text = $"Boss Timer: {bossTimer:F2}s";
            yield return null;

            if (bossTimer <= 0)
            {
                // Handle loss due to timer running out
                Debug.Log("Time's up! Player loses a life or the game.");
                // Reset or end game logic here
            }
        }
    }

    public void ApplyDamageToBoss(int damage)
    {
        bossHealth -= damage;
        Debug.Log($"Boss Health: {bossHealth}");
    }
}
