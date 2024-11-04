using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyboardSound : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.anyKeyDown)
        {
            
            SoundManager.instance.PlaySound("Keyboard");

            
        }
    }
}
