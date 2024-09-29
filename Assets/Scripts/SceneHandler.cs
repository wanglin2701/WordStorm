using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHandler
{
    public static string GetActiveSceneName()
    {
        return SceneManager.GetActiveScene().name;

    }

    public static void LoadStartScreen()
    {
        SceneManager.LoadScene("StartScreen");

    }

    public static void LoadGame()
    {
        SceneManager.LoadScene("game");
    }

    public static void LoadLoadingScreen()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

}
