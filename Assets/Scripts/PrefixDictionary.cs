using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefixDictionary : MonoBehaviour
{
    public Dictionary<string, List<string>> wordDictionary;

    void Awake()
    {
        // Initialize the prefix-word dictionary here
        wordDictionary = new Dictionary<string, List<string>>()
        {
            { "anti", new List<string>(){ "antidote", "antivirus", "antibody", "antibiotic" } },
            { "com", new List<string>(){ "communicate", "community", "combat", "computer" } },
            { "dis", new List<string>(){ "disconnect", "distribute", "discover", "disprove" } },
            { "pro", new List<string>(){ "progress", "proclaim", "protect", "problem" } },
            // Can add more prefixes and their corresponding words later on
        };
    }
}
