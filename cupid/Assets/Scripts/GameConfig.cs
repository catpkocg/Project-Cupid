using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Cupid/Game Config")]
public class GameConfig : ScriptableObject
{
    
    [SerializeField] private Vector2Int gridSize = new Vector2Int(7, 7);
    [SerializeField] private int addMaxCount = 30;
    [SerializeField] private int blockCount = 3;
    
    //funtion으로 데이터 조작
    //Property로 데이터 조작
    public Vector2Int GridSize => gridSize;
    public int AddMaxCount => addMaxCount;
    public int BlockCount => blockCount;

    public void IncrementWidth()
    {
        gridSize = new Vector2Int(gridSize.x + 1, gridSize.y);
    }

    public void DecrementWidth()
    {
        gridSize = new Vector2Int(gridSize.x - 1, gridSize.y);
    }

    public void IncrementHeight()
    {
        gridSize = new Vector2Int(gridSize.x, gridSize.y + 1);
    }

    public void DecrementHeight()
    {
        gridSize = new Vector2Int(gridSize.x, gridSize.y - 1);
    }

    public void IncrementAddMaxCount() => addMaxCount++;
    public void DecrementAddMaxCount() => addMaxCount--;
    
    public void IncrementBlockCount() => blockCount++;
    public void DecrementBlockCount() => blockCount--;
}
