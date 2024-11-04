using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public GameObject[] healthPoints;
    public string gameOverSceneName = "GameOver";
    public Animator playerAnimator;

    public Transform uiCanvas;
    public GameObject redTint;
    
    //public CameraShake cameraShake;
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        currentHealth = maxHealth;

        for (int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].SetActive(true);
        }

        if (redTint != null)
        {
            redTint.gameObject.SetActive(false);
        }   
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            // Decrease health
            currentHealth--;
     
            if (currentHealth == 0)
            {
                Die();

            }

            // Change the color and disable the corresponding circle sprite
            else if (currentHealth >= 0 && currentHealth < healthPoints.Length)
            {
              
                playerAnimator.SetBool("damaged", true);


                ChangeHealthColor(healthPoints[currentHealth], Color.red); // Change color to red to indicate damage
                Destroy(healthPoints[currentHealth], 0.5f); // Destroy the health circle after some time

                if (currentHealth == 1) // When on last life
                {
                    //add the background red tint
                    redTint.SetActive(true);
                    
                }

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

        playerAnimator.SetBool("isDead", true);

        if (redTint != null)
        {
            redTint.gameObject.SetActive(true);
        }

    }

}
