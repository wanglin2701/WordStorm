using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//SCRIPT EXPLANATION
//Checks for Player input 

public class WordInput : MonoBehaviour
{
    public WordManager WordManager;
    private TextMeshProUGUI playerInput_UI;
    private bool isStarted = false;   //Will reset once the player enters the word
    private string playerInput;
    private bool canType = true;

    private void Start()
    {


        WordManager = GameObject.Find("WordManager").GetComponent<WordManager>();
        playerInput_UI = GetComponent<TextMeshProUGUI>();
        playerInput_UI.text = "_";
        playerInput = "";


    }

    private void Update()
    {
       
        if(canType == true)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                

                if (playerInput.Length > 0)
                {
                    playerInput = playerInput.Remove(playerInput.Length - 1);

                    playerInput_UI.text = playerInput;
                }

              


            }

            else if (Input.GetKeyUp(KeyCode.Backspace))
            {

            }

            foreach (char letter in Input.inputString)  //Stores a word string of input character in case player types fast
            {



                if (Input.GetKeyDown(KeyCode.Return))  //Send data to cross reference the input of the player to the prefix and correct answer
                {
                    if (WordManager.isInputCorrect(playerInput) != "wrong")
                    {
                        Debug.Log("CORRECT");
                        WordManager.removePrefix(WordManager.isInputCorrect(playerInput));  //Call remove prefix method
                        playerInput = "";
                        playerInput_UI.text = playerInput;


                    }

                    else
                    {
                        StartCoroutine(WrongWord(0.5f));

                    }

                }

                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    //do nothing
                }

                else if (isStarted == false)
                {
                    isStarted = true;
                    playerInput = "";
                    playerInput = playerInput + letter;
                    playerInput_UI.text = playerInput;
                }

                else
                {
                    playerInput = playerInput + letter;
                    playerInput_UI.text = playerInput;


                }


            }
        }
        

       
    }

    IEnumerator WrongWord(float duration)  //Animates a simple turn red
    {
        canType = false;

        float counter = 0f;

        playerInput_UI.color = Color.red;


        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null; //Don't freeze Unity
        }
        
        playerInput_UI.color = Color.white;
        isStarted = false;
        playerInput = "_";
        playerInput_UI.text = playerInput;

        canType = true;


    }

    

}
