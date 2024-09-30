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
        
        string enemyPrefix = EnemyAI.CheckInput(word);

        // If the word is correct, trigger bullet fire
        if (!string.IsNullOrEmpty(enemyPrefix))
        {
            playerAttack.FireBullet(enemyPrefix); // Pass the correct prefix to the FireBullet method

            ComboManager.ResetComboTimer();
            UI_Manager.UpdateComboTmpro();

            ScoreManager.RewardScores(word);  //Reward Score based on the number letters
            UI_Manager.UpdateScoreTmpro();

        }

        inputField.text = ""; // Clear the input field after submission
        
        // Automatically refocus the input field after submission
        inputField.Select();
        inputField.ActivateInputField();
    }

}
