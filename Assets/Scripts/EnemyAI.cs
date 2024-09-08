using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed;

    void Start()
    {
        
    }
    
    void Update()
    {
        // Calculate direction towards the player
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Calculate the angle to rotate towards the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Move the enemy towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        // Rotate the enemy to face the player
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
