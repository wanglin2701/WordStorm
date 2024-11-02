using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField]
    private GameObject bulletPrefab;
    
    [SerializeField]
    private Transform FireGun;
    public float bulletSpeed = 20f;

    public void FireBullet(string enemyPrefix) // Pass the prefix of the target enemy
    {
        
        // Find the closest enemy
        GameObject enemy = FindClosestEnemyWithPrefix(enemyPrefix);
        Vector2 direction;

        if (enemy != null)
        {
            // Calculate direction towards the enemy
            direction = (enemy.transform.position - transform.position).normalized;
        }
        else 
        {
            direction = transform.up;
        }
        
        GameObject bullet = Instantiate(bulletPrefab, FireGun.position, Quaternion.identity);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        // Assign the enemy prefix to the bullet
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.targetPrefix = enemyPrefix; // Set the target prefix on the bullet
        
        // Rotate the bullet to face the target direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        // Apply velocity to the bullet's Rigidbody2D to make it move towards the target
        rigidbody.velocity = direction * bulletSpeed;
        
    }

  private GameObject FindClosestEnemyWithPrefix(string prefix)
    {
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();

            if (enemyAI.enemyPrefix == prefix)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }
        
        return closestEnemy;
    }
}
 //Press "enter" will trigger the bullet fire
        //if word incorrect then pressing "enter" will not trigger the bullet fire
        //press enter will not fire bullet if word is wrong
        //one press enter of correct word will trigger one bullet