using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using System.Threading;

public class Typer : MonoBehaviour
{
    //Get a word bank
    public TextMeshProUGUI wordOutput = null;  //UI Word on screen like

    private string remainingWord;
    private string currentWord = "kitty";  //Words to type or correct answer

    // Start is called before the first frame update
    void Start()
    {
        wordOutput = GameObject.Find("Typer").GetComponent<TextMeshProUGUI>();
        
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string word)
    {
        remainingWord = word;
        wordOutput.text = remainingWord;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString; //Will input everything even if u press 2 keys together
        
            if(keysPressed.Length == 1)  //If 1 key is only pressed
            {
                EnterLetter(keysPressed);
            }
        }
    }

    private void EnterLetter(string inputLetter)
    {

        if (IsCorrectLetter(inputLetter))  //Check if the letter is correct
        {
            RemoveLetter();

            //Check if the word is complete
            if (IsWordDone())
            {
                SetCurrentWord();
            }
        }

        else
        {
            StartCoroutine(WrongLetter(1f));

        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;  //check the 1st letter of the word
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);  //remove 1st char of string
        SetRemainingWord(newString);
    }

    private bool IsWordDone()
    {
       
        return remainingWord.Length == 0;
    }

    IEnumerator WrongLetter(float duration)  
    {
        float counter = 0f;

        wordOutput.color = Color.red;


        while (counter < duration)
        {
            Debug.Log("Current WaitTime: " + counter);
            counter += Time.deltaTime;
            yield return null; //Don't freeze Unity
        }
        wordOutput.color = Color.white;

    }
}
