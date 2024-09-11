using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SPAWNS WORDS, TO BE REMOVED ONCE ENEMY SPAWNER MERGE

public class WordSpawner : MonoBehaviour
{
    public GameObject wordObj;

    public Transform parent;

    public WordShow spawnWord() //returns new WordSHOW OBJ Script
    {
        Vector2 position = new Vector2(Random.Range(-2, 2), 3);   //Determine position randomly

        GameObject word = Instantiate(wordObj, position, Quaternion.identity,parent);
        WordShow wordShow = word.GetComponent<WordShow>();

        return wordShow;
    }
}
