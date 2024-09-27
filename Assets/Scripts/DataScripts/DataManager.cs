using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

//Consist of methods to read data and csv related

public static class DataManager 
{
    
    //Method to read the CSV File
    public static string[] ReadTXTFile(string separator, TextAsset txt)
    {

        if (txt.text == "")
        {
            Debug.LogError("CSV is Empty!!");
            return null;
        }

        else
        {
            string[] listofRows;  //Store the list of rows read

            if (separator == "newline")   //SEPARATOR IS A NEW LINE, DATA WILL BE SEPARATED BASED ON THAT
            {

                listofRows = txt.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < listofRows.Length; i++)  //Trim excess white spaces
                {
                    listofRows[i] = listofRows[i].Trim();
                }
            }

            else  //Will be left for more sophisticated data base (Like waves)
            {
                listofRows = null;
            }

            return listofRows;


        }
    }

    public static void WriteTXTFile(string fileName, List<string> wordListForPrefix)
    {
        bool isWriterOngoing = false;

        string path = Application.dataPath + "/Resources/FilteredWords/" + fileName + ".txt";
        Debug.Log(path);

        if(isWriterOngoing == false)
        {
            isWriterOngoing = true;

            
            // Create an instance of StreamWriter to write text to a file.
            StreamWriter sw = new StreamWriter(path);

            foreach (string word in wordListForPrefix)
            {
                sw.WriteLine(word);
            }

            sw.Close();

            isWriterOngoing = false;
        }

    }

    public static List<string> ConvertArraytoList(string[] array)
    {
        List<string> tempList = new List<string>();

        foreach(string e in array)
        {
            tempList.Add(e);
        }

        return tempList;
    }


}

