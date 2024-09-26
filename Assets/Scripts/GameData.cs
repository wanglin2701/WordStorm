using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

//Holds the data for the game (lvls, waves, dictionary, prefix etc)

public static class GameData 
{
    public static Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

    public static void SetDictionaryData(string[] dictionaryList, string[] prefixList)
    {
        int no_CharWordPrefixCorrect = 0;   //If this reaches the same as the prefx length 
        List<string> correctWords = new List<string>();
        bool prefixAndWordMatch = false;

        // 1 Prefix - Loop through all the words to see which matches)

        // 1) Go into per prefix


        foreach (string prefix in prefixList)
        {
            correctWords.Clear();
            prefixAndWordMatch = true;
            no_CharWordPrefixCorrect = 0;



            Debug.Log("Checking Prefix: " + prefix);

            // 2) go into per word
            foreach (string word in dictionaryList)
            {
                Debug.Log("Checking Word: " + word);


                foreach (char prefix_char in prefix)
                {
                    Debug.Log("Checking Prefix Char: " + prefix_char);
                    Debug.Log("Checking Word Char: " + word[no_CharWordPrefixCorrect]);


                    if (prefix_char != word[no_CharWordPrefixCorrect])
                    {
                        Debug.Log("Word Char and Prefix Char no match!");
                        Debug.Log(prefix + " and " + word + " do not match!");


                        no_CharWordPrefixCorrect = 0;  //reset the correct char counts
                        break;  //Stop comparing the prefix char with word char
                    }

                    if (no_CharWordPrefixCorrect == prefix.Length)
                    {
                        no_CharWordPrefixCorrect = 0;
                        prefixAndWordMatch = true;

                        Debug.Log("Add " + word + " to " + prefix);
                        correctWords.Add(word);

                        Debug.Log(prefix + " and " + word + " match!");
                    }


                    else
                    {
                        no_CharWordPrefixCorrect++;
                    }

                }

                if (no_CharWordPrefixCorrect == prefix.Length)
                {
                    no_CharWordPrefixCorrect = 0;
                    prefixAndWordMatch = true;

                    Debug.Log("Add " + word + " to " + prefix);
                    correctWords.Add(word);

                    Debug.Log(prefix + " and " + word + " match!");
                }

            }

            if(prefixAndWordMatch)
            {
                //    Debug.Log(prefix);
                Debug.Log(correctWords.Count);
                //    Debug.Log(dictionary.Count);
                dictionary.Add(prefix, correctWords);
                no_CharWordPrefixCorrect = 0;


            }



        }

        foreach (string word in dictionary["anti"])
        {
            Debug.Log("ANTI");
            Debug.Log(word);
        }

        foreach (string word in dictionary["com"])
        {
            Debug.Log("COM");

            Debug.Log(word);
        }

        foreach (string word in dictionary["pro"])
        {
            Debug.Log("PRO");

            Debug.Log(word);
        }

        foreach (string word in dictionary["dis"])
        {
            Debug.Log("DIS");

            Debug.Log(word);
        }
    }

    public static Dictionary<string, List<string>> GetDictionaryList()  //Get list of dictionary (include prefix)
    {
        return null;
    }

    public static List<string> GetWordListofPrefix(string prefix)  //Get the list of prefix based on what word u give it
    {
        return dictionary[prefix];
    }
}
