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

        //DebugDataReadInBuild();

        GameData.ReadGameData();

      

    }


   

    #region Debug Methods

    public void DebugDataReadInBuild()
    {
        dictionaryTXT = GameObject.Find("dictionarycheck").GetComponent<TextMeshProUGUI>();
        prefixTXT = GameObject.Find("prefixcheck").GetComponent<TextMeshProUGUI>();

        TextAsset dictionaryCSV = Resources.Load("FilteredWords/anti") as TextAsset;
        TextAsset prefixCSV = Resources.Load("FilteredWords/pro") as TextAsset;

        CheckDataInBuild(dictionaryCSV, prefixCSV);
    }

    public void CheckDataInBuild(TextAsset dictionaryCSV, TextAsset prefixCSV)  //Use this method to check if the data is passed in successfully in the build
    {

        if (dictionaryCSV != null)
        {
            dictionaryTXT.text = "anti success";
        }

        if (prefixCSV != null)
        {
            prefixTXT.text = "pro success";

        }
    }

    #endregion



}
