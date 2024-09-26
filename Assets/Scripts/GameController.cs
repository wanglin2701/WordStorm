using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Starts and loads game

public class GameController : MonoBehaviour
{
    TextMeshProUGUI dictionaryTXT;
    TextMeshProUGUI prefixTXT;

    // Start is called before the first frame update
    void Start()
    {
        //Call the method to read the data

        dictionaryTXT = GameObject.Find("dictionarycheck").GetComponent<TextMeshProUGUI>();
        prefixTXT = GameObject.Find("prefixcheck").GetComponent<TextMeshProUGUI>();


        LoadData();

    }

    public void LoadData()
    {
        //Read the .txt files strictly from the Resources folder only (IF NOT THE DATA READING WILL NOT WORK)
        TextAsset dictionaryCSV = Resources.Load("wordList") as TextAsset;
        TextAsset prefixCSV = Resources.Load("prefixList") as TextAsset;


        //Read the CSV File and store the string[] data
        string[] dictionaryData = DataManager.ReadCSVFile("newline", dictionaryCSV);
        string[] prefixData = DataManager.ReadCSVFile("newline", prefixCSV);

        //Update the GameData so that it will ahve the updated version
        GameData.SetDictionaryData(dictionaryData, prefixData);

    }

   

    #region Debug Methods
    public void CheckDataInBuild(TextAsset dictionaryCSV, TextAsset prefixCSV)  //Use this method to check if the data is passed in successfully in the build
    {

        if (dictionaryCSV != null)
        {
            dictionaryTXT.text = "dictionary success";
        }

        if (prefixCSV != null)
        {
            prefixTXT.text = "prefix success";

        }
    }

    #endregion



}
