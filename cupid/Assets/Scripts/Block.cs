using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] public int value;
    public Vector2Int Coord { get; set; }

    public int score;
    
    

    public void MoveTo()
    {
        
    }
}
