using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AroundData", menuName = "HexAround/AroundData", order = 1)]
public class AroundData : ScriptableObject
{
    public List<Around> around = new ();
}

[Serializable]
public class Around
{
    public Vector2Int aroundOneCoord;
    public Vector2Int aroundTwoCoord;
}