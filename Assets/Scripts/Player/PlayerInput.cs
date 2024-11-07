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
        //inputField.onSubmit.AddListener(SubmitWord);
        inputField.Select(); // Focus the input field at the start
        inputField.ActivateInputField(); // Activate the input field for typing

        playerAttack = FindObjectOfType<PlayerAttack>(); // Find the PlayerAttack script

        UI_Manager.SetGameScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            SubmitWord(inputField.text);
           
        }

        else if (Input.GetMouseButtonDown(0))
        {
            inputField.Select(); // Focus the input field at the start
            inputField.ActivateInputField(); // Activate the input field for typing
        }

        // Ensure the caret stays at the end of the input field text
        inputField.caretPosition = inputField.text.Length;
        inputField.selectionAnchorPosition = inputField.text.Length;
        inputField.selectionFocusPosition = inputField.text.Length;
    }
    
    void SubmitWord(string word)
    {
        
        string enemyPrefix = EnemyAI.CheckInput(word);
        inputField.text = "";

        inputField.SetTextWithoutNotify(""); // Clear the input field after submission32


        // Automatically refocus the input field after submission
        inputField.Select();
        inputField.ActivateInputField();

        // Ensure the caret stays at the end
        inputField.caretPosition = 0;

        // If the word is correct, trigger bullet fire
        if (enemyPrefix == "Word is Already Used Twice!!")
        {
            inputField.placeholder.GetComponent<TMP_Text>().color = Color.red;

            inputField.placeholder.GetComponent<TMP_Text>().text = "Word is Already Used Twice!!";
        }

        else if(enemyPrefix == "Invalid Word")
        {
            inputField.placeholder.GetComponent<TMP_Text>().color = Color.red;

            inputField.placeholder.GetComponent<TMP_Text>().text = "Wrong Word or Does not Exist in Dictionary";
        }

        else if (!string.IsNullOrEmpty(enemyPrefix))
        {
            // Clear any red text if the input is correct
            inputField.placeholder.GetComponent<TMP_Text>().text = "";  // Clear placeholder text
            inputField.placeholder.GetComponent<TMP_Text>().color = Color.white;  // Reset text color

            playerAttack.FireBullet(enemyPrefix); // Pass the correct prefix to the FireBullet method

            ComboManager.ResetComboTimer();
            UI_Manager.UpdateComboTmpro();

            ScoreManager.RewardScores(word);  //Reward Score based on the number letters
            UI_Manager.UpdateScoreTmpro();

        }

    }

}
