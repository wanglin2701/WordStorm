using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to the Rigidbody2D component of the player
    private Rigidbody2D rb;

    // Variables to store horizontal and vertical input values
    float vertical;
    float horizontal;

    // Public variable to limit diagonal movement speed (can be set in the Unity editor)
    public float speedLimit = 0.7f;

    // Public variable for the player's movement speed (can be set in Unity editor)
    public float moveSpeed;

    void Start()
    {
        // Get the Rigidbody2D component attached to the player object
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from horizontal axis (left/right or A/D keys) without smoothing
        horizontal = Input.GetAxisRaw("Horizontal");
        
        // Get input from vertical axis (up/down or W/S keys) without smoothing
        vertical = Input.GetAxisRaw("Vertical");   
    }

    void FixedUpdate() 
    {
        // Check if both horizontal and vertical inputs are not zero (i.e., player is moving diagonally)
        // Limit the inputs to prevent faster diagonal movement
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= speedLimit;
            vertical = speedLimit;
        }
        
        // Set the velocity of the Rigidbody2D based on the input direction and movement speed
        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }
}
