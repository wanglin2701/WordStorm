using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet
    private GameObject player;

    void Start()
    {
        // Locate the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Destroy the bullet after a few seconds to avoid lingering objects
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if (player != null)
        {
            // Move the bullet toward the player's position
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet has collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Access the PlayerHealth component to apply damage
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(); // Deal damage to the player
            }

            // Destroy the bullet upon impact
            Destroy(gameObject);
        }
    }
    
}
