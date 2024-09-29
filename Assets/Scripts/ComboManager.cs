using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public static class ComboManager 
{
    private static int combo = 0;

    private static bool isComboOngoing;
    private static float transparencyVal = 0;
    private static TextMeshProUGUI comboText = UI_Manager.GetComboTxt();
    public static float TimeToFade = 30f;

    public static int GetCombo()
    {
        return combo;
    }

    public static void StartComboTimer()
    {     
        if (transparencyVal >= 0)
        {
            comboText.color = new Color(comboText.color.r, comboText.color.g, comboText.color.b, transparencyVal -= TimeToFade * Time.deltaTime);

            if (transparencyVal == 0)
            {
                combo = 0;
                comboText.color = new Color(comboText.color.r, comboText.color.g, comboText.color.b, transparencyVal);

            }
        }

       
    
    }

    public static void AddCombo()
    {
        if(combo < 5)
        {
            combo++;

        }

    }
}
