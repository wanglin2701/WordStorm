using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField]
    private GameObject bulletPrefab;
    
    [SerializeField]
    private Transform FireGun;
    public float bulletSpeed = 10f;

        //Press "enter" will trigger the bullet fire
        //if word incorrect then pressing "enter" will not trigger the bullet fire
        //press enter will not fire bullet if word is wrong
        //one press enter of correct word will trigger one bullet
        


    public void FireBullet()
    {
        
        // Find the closest enemy
        GameObject enemy = FindClosestEnemy();
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

        // Rotate the bullet to face the target direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        // Apply velocity to the bullet's Rigidbody2D to make it move towards the target
        rigidbody.velocity = direction * bulletSpeed;
        
    }

  private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
