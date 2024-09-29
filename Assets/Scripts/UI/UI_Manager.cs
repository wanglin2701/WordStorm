using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//IN CHAGE OF ALL THE UI STUFF FOR THE WHOLE GAME

public static class UI_Manager 
{
 

    //Team Logo Screen Element
    private static Image[] teamLogo_array;
    private static Image teamLogo;

    //Start Screen UI Elements
    private static Button[] startScreenBTN_array;
    private static Button playBTN;
    private static Button creditsBTN;
    private static Button quitBTN;

    public static void SetTeamLogoScene()
    {
        teamLogo_array = GameObject.FindObjectsOfType<Image>();
        teamLogo = teamLogo_array[0];
    }

    public static Image GetTeamLogo()
    {
        return teamLogo;
    } 

    public static void SetStartScreenScene()
    {
        //Get the UI Elements
        startScreenBTN_array = GameObject.FindObjectsOfType<Button>();

        playBTN = startScreenBTN_array[2];
        playBTN.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
        playBTN.onClick.AddListener(SceneHandler.LoadLoadingScreen);

        creditsBTN = startScreenBTN_array[0];
        creditsBTN.GetComponentInChildren<TextMeshProUGUI>().text = "Credits";

        //creditsBTN.onClick.AddListener();  //TO BE ADDED IN FUTURE

        quitBTN = startScreenBTN_array[1];
        quitBTN.GetComponentInChildren<TextMeshProUGUI>().text = "Quit";

        //quitBTN.onClick.AddListener();  //TO BE ADDED IN FUTURE
    }

    

}
