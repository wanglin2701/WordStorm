//Peck Hsia


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Json;

//EXPLANATION FOR JSON
//Name of Json File must be = Class Name 
//For example, DialogueList is an array of DataDialogue.
//DataDialogue consist of all the different rows of data should have. Eg. StoryID, Name etc.

//STEPS
// 1) Create Excel Spreadsheet
// 2) Name json file as [Name]
// 3) Create a cs file with all the different columns there is for each row of data (MUST BE NAMED THE SAME AS json file) [DONT USE PROPERTIES, JSONUTILITY DOES WORK
// BECAUSE OF THAT]
// 4) Create a cs file that contains the array created from Step 3
// 5) Use LoadJsonFile and input the json file name
// 6) Call JsonUtility.FromJson<[Array created from STEP 4]>([Use string returned from STEP 5])

public static class JsonHandler
{
    #region JsonFiles
    public static string LoadJsonFile(string name)  //Return the whole string chunk from your json file
    {
        TextAsset file = Resources.Load<TextAsset>("JSONData/" + name);
        string fileStr = file.text;



        if (file != null) //If json file exists
        {
            string jsonStr = FixJson(name, fileStr); //Modify the string extract from the json file to fit the criteria of json utility 



            return jsonStr;
        }

        else
        {

            return null;
        }
    }

    //Takes in the whole string of contents in json File
    //className is array of what for your json.
    static string FixJson(string className, string value)
    {
        value = "{\"" + className + "\":" + value + "}";
        return value;
    }

    #endregion


    #region Save & Load (Unused)

    //public static string LoadLocalFiles(string name)
    //{
    //    string filePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + name + ".json";

    //    if (File.Exists(filePath)) //If json file exists
    //    {
    //        string str = File.ReadAllText(filePath); //Read json file and returns the whole string of contents in json file


    //        return str;
    //    }

    //    else
    //    {
    //        return null;
    //    }
    //}

    //public static bool LocalFileExist(string name)
    //{
    //    string filePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + name + ".json";

    //    if (File.Exists(filePath)) //If json file exists
    //    {



    //        return true;
    //    }

    //    else
    //    {
    //        return false;
    //    }

    //}

    //public static void SaveStatisticsToJson(Statistics obj)
    //{
    //    string json = JsonUtility.ToJson(obj); //Return the string to a file in a json format

    //    using(StreamWriter writer = new StreamWriter(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Statistic.json"))
    //    {
    //        writer.Write(json);
    //    }

    //}

    //public static void DeleteStatisticsFile(string name)
    //{
    //    File.Delete(Application.persistentDataPath + Path.AltDirectorySeparatorChar + name + ".json");
    //}

    #endregion
}
