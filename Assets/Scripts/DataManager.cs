using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UNUSED FOR NOW

public class DataManager : MonoBehaviour
{
    static DataManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

          

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
}

