using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public GameObject[] healthPoints;
    public string gameOverSceneName = "GameOver";
    
    void Start()
    {
        currentHealth = maxHealth;

        for (int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].SetActive(true);
        }   
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            // Decrease health
            currentHealth--;

            // Change the color and disable the corresponding circle sprite
            if (currentHealth >= 0 && currentHealth < healthPoints.Length)
            {
                ChangeHealthColor(healthPoints[currentHealth], Color.red); // Change color to red to indicate damage
                Destroy(healthPoints[currentHealth], 0.5f); // Destroy the health circle after some time
            }

            // Check if the player is out of health
            if (currentHealth == 0)
            {
                Die();
            }
        }
    }

    private void ChangeHealthColor(GameObject circle, Color color)
    {
        // Change the sprite renderer's color to indicate damage
        SpriteRenderer sr = circle.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = color;
        }
    }
    private void Die()
    {
        // Handle player death (e.g., game over)
        Debug.Log("Player is dead!");
        // Implement additional game over logic here

        // Change to the Game Over scene
        SceneHandler.LoadEndingScreen();
    }
}
