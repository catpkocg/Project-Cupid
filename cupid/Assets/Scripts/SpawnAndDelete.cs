using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndDelete : MonoBehaviour
{
    [SerializeField] private List<Block> threeKindsOfBlock;
    [SerializeField] private List<Block> fourKindsOfBlock;
    [SerializeField] private GameObject blockBase;
    public List<Block> allBlock = new List<Block>();
    
    public void SpawnBlock()
    {
        //var mapLocation = Map.Instance.matrix;
        var grid = Map.Instance.GetComponent<Grid>();
        for (var j = 0; j < 7; j++)
        {
            if (j % 2 == 0)
            {
                for (var i = 0; i < 7; i++)
                {
                    CreateThreeKindsOfBlock(grid,i,j);
                }
            }
            else
            {
                for (var i = 0; i < 6; i++)
                {
                    CreateThreeKindsOfBlock(grid,i,j);
                }
            }
        }
    }

    public void CreateNewBlockForEmptyPlace()
    {
        
        var grid = Map.Instance.GetComponent<Grid>();
        var deletedPos = Map.Instance.deletedPos;
        var random = Random.Range(0, 3);
        for (int i = 0; i < deletedPos.Count; i++)
        {
            if (deletedPos[i].y % 2 == 0)
            {
                var pos = grid.GetCellCenterWorld(new Vector3Int(7, deletedPos[i].y, 0));
                var block = Instantiate(threeKindsOfBlock[random], pos, Quaternion.identity);
                allBlock.Add(block);
                block.transform.SetParent(blockBase.transform);
            }
            else
            {
                var pos = grid.GetCellCenterWorld(new Vector3Int(6, deletedPos[i].y, 0));
                var block = Instantiate(threeKindsOfBlock[random], pos, Quaternion.identity);
                allBlock.Add(block);
                block.transform.SetParent(blockBase.transform);
            }
            Debug.Log(deletedPos[i]);

        }
    }
    
    

    private void CreateThreeKindsOfBlock(Grid grid, int x, int y)
    {
        var random = Random.Range(0, 3);
        var pos = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
        var block = Instantiate(threeKindsOfBlock[random], pos, Quaternion.identity);
        allBlock.Add(block);
        block.transform.SetParent(blockBase.transform);
        block.Coord = new Vector2Int(x, y);
        Map.Instance.matrix[x, y] = block;
        
    }
    
    private void CreateFourKindsOfBlock(Grid grid, int x, int y)
    {
        var random = Random.Range(0, 4);
        var pos = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
        var block = Instantiate(fourKindsOfBlock[random], pos, Quaternion.identity);
        block.transform.SetParent(blockBase.transform);
        block.Coord = new Vector2Int(x, y);
        Map.Instance.matrix[x, y] = block;
    }
    
    
    
}
