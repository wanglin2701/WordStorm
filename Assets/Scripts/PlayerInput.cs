using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    public TMP_InputField inputField; // Reference to the TMP InputField
    void Start()
    {
        inputField.onSubmit.AddListener(SubmitWord);
        inputField.Select(); // Focus the input field at the start
        inputField.ActivateInputField(); // Activate the input field for typing
    }

    void SubmitWord(string word)
    {
        EnemyAI.CheckInput(word); // Check if the input matches any enemy prefix
        inputField.text = ""; // Clear the input field after submission

        // Automatically refocus the input field after submission
        inputField.Select();
        inputField.ActivateInputField();
    }
}
