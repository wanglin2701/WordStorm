using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//SCRIPT EXPLANATION
//UI Related, Manages the text in the game
//Attached to each word

public class WordShow : MonoBehaviour
{
    public TextMeshProUGUI wordUI;

    private void Start()
    {
        wordUI = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateWordUI(string word)  //Update UI
    {
        wordUI = GetComponent<TextMeshProUGUI>();
        wordUI.text = word;
        
    }

    

    public void RemoveWord() //Remove word gameObject
    {
        Destroy(gameObject);
    }

    
}
