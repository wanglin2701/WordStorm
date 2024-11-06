using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject bossbulletPrefab;
    
    [SerializeField]
    private Transform bossWeapon;

    public void FireBullet()
    {
        
        // Locate the player by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) return;

        // Calculate direction towards the player
        Vector2 direction = (player.transform.position - bossWeapon.position).normalized;

        // Instantiate the bullet and set its position and direction
        GameObject bullet = Instantiate(bossbulletPrefab, bossWeapon.position, Quaternion.identity);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        if (rigidbody != null)
        {
            rigidbody.velocity = direction * 10f; // Adjust the speed if needed
        }

        // Rotate the bullet to face the target direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}


