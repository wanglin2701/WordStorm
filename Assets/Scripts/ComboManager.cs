using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public static class ComboManager 
{
    private static int combo = 0;

    private static float transparencyVal = 0;
    private static TextMeshProUGUI comboText = UI_Manager.GetComboTxt();
    public static float TimeToFade = 0.5f;

    public static int GetCombo()
    {
        return combo;
    }

    public static void ResetComboTimer()  //Called everytime the player successfully attacks
    {
        if (combo < 5)  //Add in the combo if it is not the maximum
        {
            combo++;

        }

        //If this is being called (Combo is started)
        transparencyVal = 1;
        comboText.color = new Color(comboText.color.r, comboText.color.g, comboText.color.b, transparencyVal);  //Set the text to opacity 100

    }

    public static void FadingOutComboTxt()   //To visually show the timer of the combo (without implementing a timer) [THIS NEEDS TO BE CALLED IN A UPDATE FUNCTION]
    {
        Debug.Log(combo);
        Debug.Log(transparencyVal);
        if (transparencyVal > 0)
        {
            comboText.color = new Color(comboText.color.r, comboText.color.g, comboText.color.b, transparencyVal -= TimeToFade * Time.deltaTime);

           
        }

        else if (transparencyVal <= 0)
        {
            combo = 0;
            transparencyVal = 1f;
            comboText.color = new Color(comboText.color.r, comboText.color.g, comboText.color.b, transparencyVal);

        }
    }
}
