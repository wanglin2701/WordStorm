using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

//Holds the data for the game (lvls, waves, dictionary, prefix etc)

public static class FilterDictionary
{
    public static Dictionary<string, List<string>> filteredDictionary = new Dictionary<string, List<string>>();

    public static void SetDictionaryData(string[] dictionaryList, string[] prefixList)
    {
        

        // 1 Prefix - Loop through all the words to see which matches)

        // 1) Go into per prefix


        foreach (string prefix in prefixList)
        {
            List<string> correctWords = new List<string>();



            // 2) go into per word
            foreach (string word in dictionaryList)
            {

                if(word.Length > prefix.Length && word.StartsWith(prefix))
                {
                    //Heres is correct
                    correctWords.Add(word);

                }

            }

            AddWordtoFilterDictionary(prefix, correctWords);  //Correct

        }



        Debug.Log("ANTI");

        foreach (string word in GetFilteredWordListofPrefix("anti"))
        {
            Debug.Log(word);
        }

        CreateTXTFile(prefixList);

    }

    public static void CreateTXTFile(string[] prefixList)
    {
        foreach(string prefix in prefixList)
        {
            DataManager.WriteTXTFile(prefix, GetFilteredWordListofPrefix(prefix));
        }
    }

    public static Dictionary<string, List<string>> GetFilteredDictionaryList()  //Get list of dictionary (include prefix)
    {
        return filteredDictionary;
    }

    public static List<string> GetFilteredWordListofPrefix(string prefix)  //Get the list of prefix based on what word u give it
    {


        if (filteredDictionary.ContainsKey(prefix))
        {
            return filteredDictionary[prefix];

        }

        else
        {
            Debug.Log("There is no such prefix key in the dictionary!");
            return null;
        }

    }

    public static void AddWordtoFilterDictionary(string prefix, List<string> correctWords)
    {
        filteredDictionary.Add(prefix, correctWords);
    }

    public static void LoadData()
    {
        //Read the .txt files strictly from the Resources folder only (IF NOT THE DATA READING WILL NOT WORK)
        TextAsset dictionaryCSV = Resources.Load("Dictionary/wordList") as TextAsset;
        TextAsset prefixCSV = Resources.Load("Dictionary/prefixList") as TextAsset;


        //Read the CSV File and store the string[] data
        string[] dictionaryData = DataManager.ReadTXTFile("newline", dictionaryCSV);
        string[] prefixData = DataManager.ReadTXTFile("newline", prefixCSV);

        //Update the GameData so that it will ahve the updated version
        SetDictionaryData(dictionaryData, prefixData);

    }
}
