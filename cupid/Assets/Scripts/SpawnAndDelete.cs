using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnAndDelete : MonoBehaviour
{
    [SerializeField] private List<Block> threeKindsOfBlock;
    [SerializeField] private List<Block> fourKindsOfBlock;
    [SerializeField] private GameObject blockBase;
    public List<Block> allBlock = new List<Block>();
    public List<Block> newBlocks;
    public List<Vector2Int> newBlocksPos;
    public List<Vector2Int> allBlockTarget;
    private void Start()
    {
        //DOTween.SetTweensCapacity( 8000, 125 );
        
    }

    public void SpawnBlock()
    {
        //var mapLocation = Map.Instance.matrix;
        var grid = Map.Instance.GetComponent<Grid>();
        for (var j = 0; j < Map.Instance.Config.GridSize.y; j++)
        {
            var height = Map.Instance.Config.GridSize.x + (j % 2 == 0 ? 0 : -1);
            for (var i = 0; i < height; i++)
            {
                SpawnRandomBlock(grid,i,j, Map.Instance.Config.BlockCount);
                Map.Instance.matrixList.Add(new Vector2(i,j));
            }
        }
    }
    
    public void CreateNewBlockForEmptyPlaceAndCheckTarget()
    {
        newBlocks = new List<Block>();
        newBlocksPos = new List<Vector2Int>();
        var grid = Map.Instance.GetComponent<Grid>();
        for (var j = 0; j < Map.Instance.Config.GridSize.y; j++)
        {
            var height = Map.Instance.Config.GridSize.x + (j % 2 == 0 ? 0 : -1);
            var currentLineNullNum = CalCuNullNum(new Vector2Int(height, j));
            for (var i = 0; i < currentLineNullNum; i++)
            {
                var random = Random.Range(0, Map.Instance.Config.BlockCount);
                var pos = grid.GetCellCenterWorld(new Vector3Int(height+i, j, 0));
                var block = Instantiate(threeKindsOfBlock[random], pos, Quaternion.identity);
                newBlocks.Add(block);
                newBlocksPos.Add(new Vector2Int((height+i) - currentLineNullNum,j));
                block.transform.SetParent(blockBase.transform);
                //Debug.Log(new Vector2Int((7+i) - currentLineNullNum,j) + " 짝수일때");
                //Debug.Log(currentLineNullNum);
            }
        }
    }

    public void CheckTarget()
    {
        allBlockTarget = new List<Vector2Int>();
        var grid = Map.Instance.GetComponent<Grid>();
        var deletedPos = Map.Instance.deletedPos;
        var random = Random.Range(0, 3);
        for (int i = 0; i < allBlock.Count; i++)
        {
            var pos = allBlock[i].Coord;
            allBlockTarget.Add(new Vector2Int(pos.x - CalCuNullNum(pos), pos.y));
        }
    }
    
    

    public void MoveAllBlocks()
    {
        Debug.Log("호출됨");
        var grid = Map.Instance.GetComponent<Grid>();
        var deletedPos = Map.Instance.deletedPos;
        
        var sequenceAnim = DOTween.Sequence();
        
        for (int i = 0; i < allBlockTarget.Count; i++)
        {
            var pos = allBlockTarget[i];
            var target = grid.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
            sequenceAnim.Join(allBlock[i].transform.DOMove(target, 0.5f).SetEase(Ease.OutCubic));
            allBlock[i].Coord = new Vector2Int(pos.x, pos.y);
            Map.Instance.matrix[pos.x, pos.y] = allBlock[i];
        }

        for (int j = 0; j < newBlocksPos.Count; j++)
        {
            var pos = newBlocksPos[j];
            var target = grid.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
            sequenceAnim.Join(newBlocks[j].transform.DOMove(target, 0.5f).SetEase(Ease.OutCubic));
            newBlocks[j].Coord = new Vector2Int(pos.x, pos.y);
            Map.Instance.matrix[pos.x, pos.y] = newBlocks[j];
        }
        
        //Debug.Log(allBlock.Count);
        
        for (int k = 0; k < newBlocks.Count; k++)
        {
            allBlock.Add(newBlocks[k]);
        }
        
        //Debug.Log(allBlock.Count);
        
        sequenceAnim.OnComplete(ChangeStatesForMove);
    }

    public void ChangeStatesForMove()
    {
        Debug.Log("이거 안들어옴?");
        GameManager.Instance.State = States.ReadyForInteraction;
    }

    private int CalCuNullNum(Vector2Int standard)
    {
        var nullNum = 0;
        if (standard.y % 2 == 0)
        {
            for (int i = 0; i < standard.x; i++)
            {
                if (Map.Instance.matrix[i, standard.y] == null)
                {
                    nullNum++;
                }
            }
        }
        else
        {
            for (int i = 0; i < standard.x; i++)
            {
                if (Map.Instance.matrix[i, standard.y] == null)
                {
                    nullNum++;
                }
            }
        }

        return nullNum;
    }

    private void SpawnRandomBlock(Grid grid, int x, int y, int randomCount)
    {
        var random = Random.Range(0, randomCount);
        var pos = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
        var block = Instantiate(threeKindsOfBlock[random], pos, Quaternion.identity);
        allBlock.Add(block);
        block.transform.SetParent(blockBase.transform);
        block.Coord = new Vector2Int(x, y);
        Map.Instance.matrix[x, y] = block;
        
    }
}
