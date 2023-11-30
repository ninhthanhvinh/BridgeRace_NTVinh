using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Bricks", menuName = "ScriptableObjects/Bricks", order = 1)]
public class Bricks : ScriptableObject
{
    public BrickType brickType;
    public Material material;
}
