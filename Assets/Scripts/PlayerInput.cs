using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    public TMP_InputField inputField; // Reference to the TMP InputField
    private PlayerAttack playerAttack; // Reference to PlayerAttack script

    void Start()
    {
        inputField.onSubmit.AddListener(SubmitWord);
        inputField.Select(); // Focus the input field at the start
        inputField.ActivateInputField(); // Activate the input field for typing

        playerAttack = FindObjectOfType<PlayerAttack>(); // Find the PlayerAttack script
    }

    void SubmitWord(string word)
    {
        
        // Check if the word matches any enemy prefix
        bool isWordCorrect = EnemyAI.CheckInput(word); // Modify CheckInput to return a boolean

        // If the word is correct, trigger bullet fire
        if (isWordCorrect)
        {
            playerAttack.FireBullet(); // Call FireBullet in PlayerAttack
        }
        
        inputField.text = ""; // Clear the input field after submission

        // Automatically refocus the input field after submission
        inputField.Select();
        inputField.ActivateInputField();
    }
}
