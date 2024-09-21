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
            { "anti", new List<string>(){ "antidote", "antivirus", "antibody", "antibiotic", "anticipate", "antic", "antisocial", "anticrime", "antitrust", "antiseptic" } },
            { "com", new List<string>(){ "communicate", "community", "combat", "computer", "complete", "commit", "combo", "comic" } },
            { "dis", new List<string>(){ "disconnect", "distribute", "discover", "disprove", "dislike", "disguise", "dish", "distance", "disagree" } },
            { "pro", new List<string>(){ "progress", "proclaim", "protect", "problem", "probate", "proud", "prove", "promise", "professor", "program" ,"proof","process", "project", "property" } },
            // Can add more prefixes and their corresponding words later on
        };
    }
}
