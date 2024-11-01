using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;


//Starts and loads game
//Handles the loading screen, other ui stuff is at the UI_Manager

public class GameController : MonoBehaviour
{
    private static TextMeshProUGUI dictionaryTXT;
    private static TextMeshProUGUI prefixTXT;

    //Loading Screen UI Element
    private static Slider progress_bar;

    private static string activeScene;

    public Animator playerAnimator;


    private void Awake()
    {


        activeScene = SceneHandler.GetActiveSceneName();
        if (activeScene == "LoadingScreen")
        {
            //Get required needed UI Elements 
            progress_bar = GameObject.FindAnyObjectByType<Slider>();
            GameData.ReadGameData();

            //Check data
            Debug.Log("=========GET RANDOM PREFIX BASED ON LETTERS==============");
            Debug.Log(GameData.GetEnemyWaveList().EnemyWave[2].enemyCount);
            Debug.Log(GameData.GetEnemyWaveByNo(2).waveNo);
            Debug.Log(GameData.GetEnemyByType("Normal").desc);
            Debug.Log(GameData.GetEnemyByType("Armored").desc);



        }

        else if (activeScene == "game")
        {
            UI_Manager.SetInputField();
            playerAnimator = GameObject.Find("Player").GetComponent<Animator>();

        }

    }

    // Start is called before the first frame update
    private void Start()
    {
        //Call the method to read the data

        //DebugDataReadInBuild();

        if (activeScene == "LoadingScreen")
        {
            LoadingScreen.IncrementProgress(progress_bar, 1.00f);
        }

        else if(activeScene == "TeamLogo")
        {
            UI_Manager.SetTeamLogoScene();
        } 

        else if(activeScene == "StartScreen")
        {
            UI_Manager.SetStartScreenScene();
        }

        else if(activeScene == "game")
        {
            UI_Manager.SetGameScene();

        }

        else if(activeScene == "GameOver")
        {
            UI_Manager.SetGameoverScreen();

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
            StartCoroutine(WaitforSecond(4f, "FadeAnim"));
            FadeInOut.StartFadingOut(teamLogo);
            StartCoroutine(WaitforSecond(2.5f, "FadeAnim"));
        }

        else if(SceneHandler.GetActiveSceneName() == "game")
        {
   
            ComboManager.FadingOutComboTxt(); //Keeps updating the indication for the combo timer (Which is fading out)
           

            //Checking Input
            if (Input.GetMouseButtonDown(0))
            {
                UI_Manager.SetInputField();
            }

            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
            {
                playerAnimator.SetBool("damaged", false);
            }

            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death") && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                playerAnimator.SetBool("isDead", false);

                Debug.Log("Helloo");
                // Change to the Game Over scene
                SceneHandler.LoadEndingScreen();
            }
        }
    }

    static IEnumerator WaitforSecond(float duration, string destination)  //1f 1 second
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            

            yield return null; //Don't freeze Unity

           
        }
        //Debug.Log(counter);

        if(counter >= duration)
        {
           if(destination == "FadeAnim")
           {
                SceneHandler.LoadStartScreen();

           }

           else if (destination == "Combo")
           {
                ComboManager.FadingOutComboTxt();
           }


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
