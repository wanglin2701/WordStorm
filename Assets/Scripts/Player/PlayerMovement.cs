using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to the Rigidbody2D component of the player
    private Rigidbody2D rb;

    public Animator playerAnimator;


    private bool facingRight = true; // To track the current facing direction

    // Variables to store horizontal and vertical input values
    private Vector2 movement;


    // Public variable to limit diagonal movement speed (can be set in the Unity editor)
    public float speedLimit = 0.7f;

    // Public variable for the player's movement speed (can be set in Unity editor)
    public float moveSpeed;

    public float screenLeft;  // Adjust based on the screen's left boundary in game scene
    public float screenRight;  // Adjust based on the screen's right boundary in game scene
    public float screenTop;    // Adjust based on the screen's top boundary in game scene
    public float screenBottom; // Adjust based on the screen's bottom boundary in game scene


    void Start()
    {
        // Get the Rigidbody2D component attached to the player object
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        
    }

    void Update()
    {
        // Get input from horizontal axis (left/right or A/D keys) without smoothing
        movement.x = Input.GetAxisRaw("Horizontal");

        // Get input from vertical axis (up/down or W/S keys) without smoothing
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
    }

    void FixedUpdate() 
    {
        // Check if both horizontal and vertical inputs are not zero (i.e., player is moving diagonally)
        //// Limit the inputs to prevent faster diagonal movement
        //if (horizontal != 0 && vertical != 0)
        //{

        //    horizontal *= speedLimit;
        //    vertical = speedLimit;
        //}

        // Move the player
        rb.velocity = movement * moveSpeed;

        if (movement.magnitude == 0)
        {
            playerAnimator.SetBool("isMoving", false);
        }
        else
        {
            playerAnimator.SetBool("isMoving", true);
        }

        // Check if the player is moving horizontally
        if (movement.x < 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x > 0 && facingRight)
        {
            Flip();
        }

        // Constrain player to screen boundaries
        ConstrainToScreen();

        
    }

    void Flip()
    {
        // Toggle the facing direction
        facingRight = !facingRight;

        // Multiply the x scale by -1 to flip the sprite
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Method to constrain player position within defined screen boundaries
    void ConstrainToScreen()
    {
        Vector3 position = transform.position;

        // Constrain player within the screen bounds
        position.x = Mathf.Clamp(position.x, screenLeft, screenRight);
        position.y = Mathf.Clamp(position.y, screenBottom, screenTop);

        // Apply the clamped position to the player
        transform.position = position;
    }
}
