using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackEndManager : MonoBehaviour
{
    //Purely to filter and generate the txt file


    // Start is called before the first frame update
    void Awake()
    {
        FilterDictionary.LoadData();

    }

   
}
