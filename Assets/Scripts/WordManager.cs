using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Overlooks the scene, communicates with other scripts

public class WordManager : MonoBehaviour
{

    public WordSpawner wordSpawner;

    #region Prefix & Dictionary List
    [SerializeField]
    private static string[] prefList =
    {
        "hel", "ho"
    };

    private static string[] dictList =
    {
        "hello", "help", "home", "hell"
    };

    private static List<string> current_preflist = new List<string>();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        wordSpawner = GetComponent<WordSpawner>();

        addPrefix();
        addPrefix();
        addPrefix();
    }

    public void addPrefix()   //Placeholder, will be replaced with enemy spawning TO BE REMOVED
    {
        string randomPref = WordManager.GetRandomPrefix();
        Word word = new Word(randomPref, wordSpawner.spawnWord()); //Create word obj
        current_preflist.Add(randomPref);

    }

   public void removePrefix(string prefix)   //Kill the gameobject
    {
        foreach (string c in current_preflist)
        {
            Debug.Log(c);
        }

        TextMeshProUGUI[] listofPrefix = GameObject.FindObjectsOfType<TextMeshProUGUI>();   //Get all gameobject that is textmpro
        
        foreach(TextMeshProUGUI txt in listofPrefix)
        {
            if (txt.text == prefix)  //Get the text and compare with prefix to be removed
            {
                txt.GetComponent<WordShow>().RemoveWord();

                if (txt.GetComponent<WordShow>() )
                {
                    current_preflist.Remove(txt.text);
                }
            }
        }

        foreach(string c in current_preflist)
        {
            Debug.Log(c);
        }
    }

    

    public static string GetRandomPrefix() //Returns a random prefix
    {
        int idx = Random.Range(0, prefList.Length);
        string randomWord = prefList[idx];

        return randomWord;
    }

    #region Check Input Methods

    public static string isInputCorrect(string input)
    {
        if (existsInDictionary(input) == false)  //If input is not in the dictionary
        {

            return "wrong";
        }

        else  //the input is in the Dictionary
        {
            if (isWordPrefMatch(input) != "wrong")  //Prefix exists in the word
            {
                //Input matches and is correct
                return isWordPrefMatch(input);

            }

            else
            {

                return "wrong";
            }
        }


    }

    public static bool existsInDictionary(string input)   //Checks first if the input exists in the Dictionary
    {
        foreach (string word in dictList)
        {
            if (word == input)
            {
                return true;
            }


        }

        return false;
    }

    public static string isWordPrefMatch(string word)   //Method to check if the given word matches the prefix provided
    {
        //Check each character of the word
        char[] word_char = word.ToCharArray();
        int counter = 0;  //Counter that increments if the character in word mathes with the prefix


        foreach (string prefix in current_preflist)   //Run through prefix list
        {
            Debug.Log("Prefix checking rn is " + prefix);
            Debug.Log("Counter is " + counter);
            Debug.Log("Prefix Length " + prefix.Length);




            counter = 0;

            foreach (char c in prefix)  //Run through the letters in prefix
            {
                if (c == word_char[counter])
                {
                    Debug.Log("Currently checking prefix letter " + c);
                    Debug.Log("Currently checking word char " + word_char[counter]);

                    counter++;  //move to the next letter in the word

                    if (counter == prefix.Length)  //if counter == prefix counter meaning correct 
                    {


                        return prefix;
                    }
                }


                else
                {
                    break; //move to next prefix
                }


            }




        }

        return "wrong";


    }

    #endregion

}
