using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public int bulletDamage = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        if(playerAttack == null)
        {
            playerAttack = FindObjectOfType<PlayerAttack>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyAI enemy = collision.GetComponent<EnemyAI>();

        if (enemy != null)
        {
            // Optionally, call a method on the enemy to deal damage
            enemy.TakeDamage(bulletDamage); // Assuming the enemy has a TakeDamage method

            // Destroy the bullet after hitting the enemy
            Destroy(gameObject);
        }
    }

    
}
