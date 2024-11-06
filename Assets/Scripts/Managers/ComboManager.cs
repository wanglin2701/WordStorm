using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public static class ComboManager 
{
    private static int combo = 0;

    private static float transparencyVal = 0;
    private static TextMeshProUGUI comboText = UI_Manager.GetComboTxt();
    private static TextMeshProUGUI comboWordText = UI_Manager.GetComboWordTxt();

    public static float TimeToFade = 0.2f;

    private static bool isComboOngoing = false;

    public static int GetCombo()
    {
        return combo;
    }

    public static void ResetCombo()
    {
        combo = 0;
    }

    public static void ResetComboTimer()  //Called everytime the player successfully attacks
    {
        isComboOngoing = true;

        if (combo < 5)  //Add in the combo if it is not the maximum
        {
            combo++;

        }

        //If this is being called (Combo is started)
        transparencyVal = 1;
        comboWordText.color = new Color(comboWordText.color.r, comboWordText.color.g, comboWordText.color.b, transparencyVal);  //Set the text to opacity 100
        comboText.color = new Color(comboText.color.r, comboText.color.g, comboText.color.b, transparencyVal);  //Set the text to opacity 100

        isComboOngoing = false;


    }

    public static void FadingOutComboTxt()   //To visually show the timer of the combo (without implementing a timer) [THIS NEEDS TO BE CALLED IN A UPDATE FUNCTION]
    {

        if (combo == 0)
        {
            transparencyVal = 1f;
        }

        else
        {
            if (transparencyVal > 0)
            {
                comboWordText.color = new Color(comboWordText.color.r, comboWordText.color.g, comboWordText.color.b, transparencyVal);  //Set the text to opacity 100
                comboText.color = new Color(comboText.color.r, comboText.color.g, comboText.color.b, transparencyVal -= TimeToFade * Time.deltaTime);
            }

            else if (transparencyVal <= 0)
            {
                combo = 0;
                UI_Manager.UpdateComboTmpro();
                transparencyVal = 1f;
                comboText.color = new Color(comboText.color.r, comboText.color.g, comboText.color.b, transparencyVal);
                comboWordText.color = new Color(comboWordText.color.r, comboWordText.color.g, comboWordText.color.b, transparencyVal);  //Set the text to opacity 100



            }
        }
      
        
    }

    public static float GetTransparencyVal()
    {
        return transparencyVal;
    }

    public static bool isComgoingCurrentlyRunning()
    {
        return isComboOngoing;
    }

    public static float SetTransParencyVal(float val)
    {
        return transparencyVal = 1f;
    }
}
