using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level 
{
    public int levelNumber;

    public GameObject levelObject;
    public Transform[] spawnPositions;
    public Transform[] initGoal;
    public BrickSpawner[] spawner;

}
