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

    public static void LoadTest()
    {
        SceneManager.LoadScene("PeckHsia");
    }
}