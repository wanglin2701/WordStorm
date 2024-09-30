using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


//Starts and loads game
//Handles the loading screen, other ui stuff is at the UI_Manager

public class GameController : MonoBehaviour
{
    private static TextMeshProUGUI dictionaryTXT;
    private static TextMeshProUGUI prefixTXT;

    //Loading Screen UI Element
    private static Slider progress_bar;

    private static string activeScene;

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

        else if(SceneHandler.GetActiveSceneName() == "game")
        {
            UI_Manager.SetGameScene();
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
            StartCoroutine(WaitforSecond(4f));
            FadeInOut.StartFadingOut(teamLogo);
            StartCoroutine(WaitforSecond(2.5f));
        }

        else if(SceneHandler.GetActiveSceneName() == "game")
        {
            ComboManager.FadingOutComboTxt();  //Keeps updating the indication for the combo timer (Which is fading out)
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
        Debug.Log(counter);
        if(counter >= duration && duration == 2.5f)
        {
           
            SceneHandler.LoadStartScreen();

            
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
