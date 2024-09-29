using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    private static int score = 0;
    private static int numberLetters;

    public static void RewardScores(string correectInput, int scoreMultiplyer)
    {
        numberLetters = correectInput.Length;

        int multiplyerScore = 2 * scoreMultiplyer;
        score += multiplyerScore * numberLetters;


    }

    public static int GetPlayerScore()
    {
        return score;
    }
}
