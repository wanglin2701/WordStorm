using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//IN CHAGE OF ALL THE UI STUFF FOR THE WHOLE GAME

public static class UI_Manager 
{
 

    //Team Logo Screen Element
    private static Image teamLogo;

    //Start Screen UI Elements
    private static Button[] startScreenBTN_array;
    private static Button playBTN;
    private static Button creditsBTN;
    private static Button quitBTN;

    //Game UI Elements
    private static TextMeshProUGUI scoreTxt;
    private static TextMeshProUGUI comboTxt;
    private static TextMeshProUGUI comboWordTxt;
    
    private static TMP_InputField inputField;

    //Gameover UI
    private static Button homeBTN;

    public static void SetTeamLogoScene()
    {
        teamLogo = GameObject.FindObjectOfType<Image>();
    }

    public static Image GetTeamLogo()
    {
        return teamLogo;
    } 

    public static void SetStartScreenScene()
    {
        //Get the UI Elements
        startScreenBTN_array = GameObject.Find("Buttons").GetComponentsInChildren<Button>();

        playBTN = startScreenBTN_array[0];
        playBTN.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
        playBTN.onClick.AddListener(SceneHandler.LoadLoadingScreen);

        creditsBTN = startScreenBTN_array[1];
        creditsBTN.GetComponentInChildren<TextMeshProUGUI>().text = "Credits";

        //creditsBTN.onClick.AddListener();  //TO BE ADDED IN FUTURE

        quitBTN = startScreenBTN_array[2];
        quitBTN.GetComponentInChildren<TextMeshProUGUI>().text = "Quit";

        //quitBTN.onClick.AddListener();  //TO BE ADDED IN FUTURE
    }

    public static void SetGameScene()
    {
        scoreTxt = GameObject.Find("ScoreTxt").GetComponentInChildren<TextMeshProUGUI>();
        comboTxt = GameObject.Find("ComboTxt").GetComponentInChildren<TextMeshProUGUI>();
        comboWordTxt = GameObject.Find("ComboWord").GetComponent<TextMeshProUGUI>();
    }

    public static void UpdateScoreTmpro()
    {
        Debug.Log(ScoreManager.GetPlayerScore().ToString());

        scoreTxt.text = ScoreManager.GetPlayerScore().ToString();
    }

    public static void UpdateComboTmpro()
    {
        comboTxt.text = ComboManager.GetCombo().ToString();
    }

    public static TextMeshProUGUI GetComboTxt()
    {
        return comboTxt;
    }

    public static void SetInputField()
    {
        inputField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        inputField.ActivateInputField();
    }


    public static TextMeshProUGUI GetComboWordTxt()
    {
        return comboWordTxt;
    }

    public static void SetGameoverScreen()
    {
        homeBTN = GameObject.Find("HomeBTN").GetComponent<Button>();
        homeBTN.GetComponentInChildren<TextMeshProUGUI>().text = "Back to Home";
        homeBTN.onClick.AddListener(SceneHandler.LoadStartScreen);
    }



}
