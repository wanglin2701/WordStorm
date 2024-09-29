using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class FadeInOut 
{
    private static bool isFadingIn = false;
    private static bool isFadingOut = false;

    public static float TimeToFade = 0.5f;

    private static float transparencyVal = 0;

    public static void StartFadeAnimation(Image obj)
    {


        if (isFadingOut == false)  //For faing in
        {
            isFadingIn = true;

            if (transparencyVal < 0.5)
            {
                obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, transparencyVal += TimeToFade * Time.deltaTime);

                if (transparencyVal >= 0.5)
                {

                    isFadingIn = false;
                }

            }

        }

    }

    public static void StartFadingOut(Image obj)
    {
        if (isFadingIn == false)  //For fading out
        {
            isFadingOut = true;
            Debug.Log(transparencyVal);
            if (transparencyVal >= 0)
            {
                obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, transparencyVal -= TimeToFade * Time.deltaTime);

                if (transparencyVal == 0)
                {

                    isFadingOut = false;

                }

            }

        }
    }


   



}
