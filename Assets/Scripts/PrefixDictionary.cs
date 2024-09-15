using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefixDictionary : MonoBehaviour
{
    public Dictionary<string, List<string>> wordDictionary;

    void Awake()
    {
        // Initialize your prefix-word dictionary here
        wordDictionary = new Dictionary<string, List<string>>()
        {
            { "anti", new List<string>(){ "antidote", "antivirus", "antibody" } },
            { "com", new List<string>(){ "communicate", "community", "combat" } },
            { "dis", new List<string>(){ "disconnect", "distribute", "discover" } },
            { "pro", new List<string>(){ "progress", "proclaim", "protect" } },
            // Can add more prefixes and their corresponding words 
        };
    }
}
