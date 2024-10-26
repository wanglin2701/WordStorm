using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public static class GameData 
{
    private static Dictionary<string, List<string>> WordStormDictionary = new Dictionary<string, List<string>>();
    private static Dictionary<string, List<string>> prefixesByType = new Dictionary<string, List<string>>();

    private static string[] prefixList;

    private static EnemyWaveList enemyWaveList;
    private static EnemyList enemyList;

    public static void ReadGameData()  //Read all the Game Data
    {
        ReadDictionaryTextFile();
        SetEnemyWave();
        SetEnemy();
    }

    #region Txt File Reading

    public static void ReadDictionaryTextFile()
    {
        //Get the list of prefix first (So you know what text files to get)
        TextAsset prefixTXT = Resources.Load("Dictionary/prefixList") as TextAsset;
        prefixList = DataManager.ReadTXTFile("newline", prefixTXT);

        SetDictionaryGameData(prefixTXT);

        prefixesByType["Normal"] = new List<string>();
        prefixesByType["Armored"] = new List<string>();
        foreach (string prefix in prefixList)
        {
            if (prefix.Length <= 3) 
                prefixesByType["Normal"].Add(prefix);
            else 
                prefixesByType["Armored"].Add(prefix);
        }

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

    #endregion

    #region Get Methods

    //Get Prefix List
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

    // public static string GetRandomPrefix(string type)
    // {
    //     if (!prefixesByType.ContainsKey(type) || prefixesByType[type].Count == 0)
    //         return null;
    //     List<string> validPrefixes = prefixesByType[type];
    //     return validPrefixes[Random.Range(0, validPrefixes.Count)];
    // }

    public static (string prefix, int enemyID) GetRandomPrefixBasedOnNumberLetters(int numberLetter)  //Returns a random prefix based on the number of letter prefix needed
    {
        //Get all the prefix based on the number of letters first
        List<string> filteredPrefixes = new List<string>();
        
        foreach(string prefix in prefixList)
        {
            if(prefix.Length == numberLetter)
            {
                filteredPrefixes.Add(prefix);
            }
        }

        // If no prefix found for given length, return (null, 0)
        if (filteredPrefixes.Count == 0)
            return (null, 0);

        int randomIndex = Random.Range(0, filteredPrefixes.Count);
        string selectedPrefix = filteredPrefixes[randomIndex];

        // Determine enemy ID
        int enemyID = selectedPrefix.Length <= 3 ? 101 : 102;

        return (selectedPrefix, enemyID);

    }

    //Get Enemy Wave Methods
    public static EnemyWaveList GetEnemyWaveList()
    {
        return enemyWaveList;
    }

    public static EnemyWave GetEnemyWaveByNo(int waveNo)  //Returns wave information based on the wave number
    {
        foreach(EnemyWave wave in enemyWaveList.EnemyWave)
        {
            if(wave.waveNo == waveNo)
            {
                return wave;
            }
        }

        return null;
    }

    //Get Enemy Methods
    public static Enemy GetEnemyByType(string type)
    {
        foreach(Enemy enemy in enemyList.Enemy)
        {
            if(enemy.enemyType == type)
            {
                return enemy;
            }
        }
        return null;
    }

public static Enemy GetEnemyByID(int ID)
    {
        foreach(Enemy enemy in enemyList.Enemy)
        {
            if(enemy.enemyID == ID)
            {
                return enemy;
            }
        }
        return null;
    }

    
    #endregion

    #region JSON File Read

    public static void SetEnemyWave()
    {
        string enemyWaveString;
        enemyWaveString = JsonHandler.LoadJsonFile("EnemyWave");  
        enemyWaveList = JsonUtility.FromJson<EnemyWaveList>(enemyWaveString);   //Creates the obj based on the json string
    }

    public static void SetEnemy()
    {
        string enemyString;
        enemyString = JsonHandler.LoadJsonFile("Enemy");
        enemyList = JsonUtility.FromJson<EnemyList>(enemyString);   //Creates the obj based on the json string
    }

    #endregion
    
}
