using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager instance;

    [SerializeField] private List<Prefix> PrefixList;


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
        InputPrefixData();
        Debug.Log(PrefixList[0].prefix);
        Debug.Log(PrefixList[1].prefix);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Prefix Data Setup

    private void InputPrefixData()
    {
        PrefixList = new List<Prefix>();

        //Can call all the method to input all the prefix data here
        AddtoPrefixList(3,"hel");
        AddtoPrefixList(2, "ho");

    }

    public void AddtoPrefixList(int numberLetter, string prefix)
    {
        //Create a prefix obj and store the prefix
        Prefix newPrefix = new Prefix();
        newPrefix.numberLetters = numberLetter;
        newPrefix.prefix = prefix;

        //Add to prefix list
        PrefixList.Add(newPrefix);
    }

    public void RemovePrefix(Prefix prefixObj)
    {
        PrefixList.Remove(prefixObj);
    }

    public List<Prefix> GetPrefixList()
    {
        return PrefixList;
    }

    #endregion
}

