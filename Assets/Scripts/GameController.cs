using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


//Starts and loads game
//Handles the loading screen, other ui stuff is at the UI_Manager

public class GameController : MonoBehaviour
{
    static TextMeshProUGUI dictionaryTXT;
    static TextMeshProUGUI prefixTXT;

    string activeScene;

    //Loading Screen UI Element
    private static Slider progress_bar;

    private void Awake()
    {
        activeScene = SceneHandler.GetActiveSceneName();
        if (SceneHandler.GetActiveSceneName() == "LoadingScreen")
        {
            //Get required needed UI Elements 
            progress_bar = GameObject.FindAnyObjectByType<Slider>();
            GameData.ReadGameData();

        }

    }

    // Start is called before the first frame update
    private void Start()
    {
        //Call the method to read the data

        //DebugDataReadInBuild();

        if (SceneHandler.GetActiveSceneName() == "LoadingScreen")
        {
            LoadingScreen.IncrementProgress(progress_bar, 1.00f);
        }

        else if(SceneHandler.GetActiveSceneName() == "TeamLogo")
        {
            UI_Manager.SetTeamLogoScene();
        } 

        else if(SceneHandler.GetActiveSceneName() == "StartScreen")
        {
            UI_Manager.SetStartScreenScene();
        }

    }

    private void Update()
    {
        if(SceneHandler.GetActiveSceneName() == "LoadingScreen")
        {
            LoadingScreen.StartLoadingBar(progress_bar);  //Start Loading Screen
        }

        else if(SceneHandler.GetActiveSceneName() == "TeamLogo")
        {
            Image teamLogo = UI_Manager.GetTeamLogo();

            FadeInOut.StartFadeAnimation(teamLogo);
            StartCoroutine(WaitforSecond(3f));
            FadeInOut.StartFadingOut(teamLogo);
            StartCoroutine(WaitforSecond(10f));
            SceneHandler.LoadStartScreen();
        }
    }

    static IEnumerator WaitforSecond(float duration)  //1f 1 second
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null; //Don't freeze Unity
        }

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
