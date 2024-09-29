using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LoadingScreen
{
    //private Button TestBTN;

    private static float FillSpeed = 0.5f;

    private static float targetProgress = 1;

    //void Start()
    //{
    //    TestBTN = GameObject.Find("TestBTN").GetComponent<Button>();
    //    TestBTN.onClick.AddListener(SceneHandler.LoadGame);
    //}

    public static void StartLoadingBar(Slider progress_bar)
    {
        if (progress_bar.value < targetProgress)
        {
            //Debug.Log("ProgressBar: " + progress_bar.value);

            progress_bar.value += FillSpeed * Time.deltaTime;
        }

        if (progress_bar.value == 1)
        {
            SceneHandler.LoadGame();
        }
    }

    //Update loading bar
    public static void IncrementProgress(Slider progress_bar, float newProgress)
    {
        targetProgress = progress_bar.value + newProgress;
    }

}
