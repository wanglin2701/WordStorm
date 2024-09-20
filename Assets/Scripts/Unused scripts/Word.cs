using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//SCRIPT EXPLANATION
//Basic word class base

public class Word
{
    public string word;

    public WordShow wordShow;  //Script to update the words UI script


    public Word(string newWord, WordShow newWordShow)  //Constructor
    {
        word = newWord;
        //typeIdx = 0;
        wordShow = newWordShow;
        wordShow.UpdateWordUI(word);  //Updates UI
    }

  
}
