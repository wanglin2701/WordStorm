using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData 
{
    public static Dictionary<string, List<string>> WordStormDictionary = new Dictionary<string, List<string>>();

    public static string[] prefixList;

    public static void ReadGameData()  //Read all the Game Data
    {
        ReadDictionaryTextFile();
    }

    public static void ReadDictionaryTextFile()
    {
        //Get the list of prefix first (So you know what text files to get)
        TextAsset prefixTXT = Resources.Load("Dictionary/prefixList") as TextAsset;
        prefixList = DataManager.ReadTXTFile("newline", prefixTXT);

        SetDictionaryGameData(prefixTXT);

    }

    public static void SetDictionaryGameData(TextAsset prefixTXT)
    {
        //Loop through the list of prefix to find the text file
        foreach (string prefix in prefixList)
        {
            //Read the file based on that 1 prefix
            TextAsset current_Prefix = Resources.Load("FilteredWords/" + prefix) as TextAsset;
            string[] current_Prefix_WordArray;
            List<string> currentPrefix_WordList = new List<string>();

            //Store the list of correct words for 1 prefix
            current_Prefix_WordArray = DataManager.ReadTXTFile("newline", current_Prefix);

            currentPrefix_WordList = DataManager.ConvertArraytoList(current_Prefix_WordArray);

            if (WordStormDictionary.ContainsKey(prefix))
            {
                WordStormDictionary[prefix] = currentPrefix_WordList;  //Update the word list
            }

            else  //If the prefix key does not exist in the dictionary
            {
                //Add to Dictionary
                WordStormDictionary.Add(prefix, currentPrefix_WordList);
            }
      

          

        }

      
    }

    public static string[] GetPrefixList()
    {
        return prefixList;
    }

    public static Dictionary<string, List<string>> GetWordStormDictionary()
    {
        return WordStormDictionary;
    }

    public static List<string> GetWordListByPrefix(string prefix)
    {
        if (WordStormDictionary.ContainsKey(prefix))
        { 


            return WordStormDictionary[prefix];

        }

        else
        {
            Debug.Log("No such prefix");
            return null;
        }

    }
}
