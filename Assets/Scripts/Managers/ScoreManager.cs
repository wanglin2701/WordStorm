using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    private static float score = 0;
    private static int numberLetters;
    private static float scoreMultiplyer;

    public static int RewardScores(string correectInput)
    {
        numberLetters = correectInput.Length;

        //multiplyer determined by combo / 1
        scoreMultiplyer = ComboManager.GetCombo() / 1.5f;

        //Add 2 points for each letter
        score += (2 * scoreMultiplyer) * numberLetters;


        Debug.Log(ComboManager.GetCombo().ToString());  
        score = score * scoreMultiplyer;
        Debug.Log("Score Multiplyer " + scoreMultiplyer);

        int finalScore = (int)score;

        return finalScore;
    }

    public static int GetPlayerScore()
    {
        return (int)score; 
    }
}
