using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private Button TestBTN;

    // Start is called before the first frame update
    void Start()
    {
        TestBTN = GameObject.Find("TestBTN").GetComponent<Button>();
        TestBTN.onClick.AddListener(SceneHandler.LoadTest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
