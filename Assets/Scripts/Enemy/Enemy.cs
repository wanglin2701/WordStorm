using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public int enemyNo;
    public int enemyID;
    public string desc;
    public string enemyType;
    public int health;
    public int speed;
    public int stoppingDist;
    public int enemyAvoidanceDist;
    public int separationForce;
}
