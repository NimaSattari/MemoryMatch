using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/Levels", order = 1)]
public class LevelScriptableObject : ScriptableObject
{
    [Header("Time To Complete Each Level")]
    public float timeToComplete;
    [Header("Number Of Cards To Generate")]
    public int howManyCards;
    [Header("Back And Front Of Cards Sprites")]
    public Sprite[] backSprites, frontSprites;
}
