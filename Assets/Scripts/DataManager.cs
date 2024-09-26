using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Consist of methods to read data and csv related

public static class DataManager 
{
    
    //Method to read the CSV File
    public static string[] ReadCSVFile(string separator, TextAsset csv)
    {

        if (csv.text == "")
        {
            Debug.LogError("CSV is Empty!!");
            return null;
        }

        else
        {
            string[] listofRows;  //Store the list of rows read

            if (separator == "newline")   //SEPARATOR IS A NEW LINE, DATA WILL BE SEPARATED BASED ON THAT
            {

                listofRows = csv.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

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
    

  
}

