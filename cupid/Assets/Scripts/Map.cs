using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wayway.Engine;
using Wayway.Engine.Singleton;

public class Map : MonoSingleton<Map>
{
    [SerializeField] public GameObject cell;
    [SerializeField] public GameObject cellBase;
    [SerializeField] private GameObject cam;

    public GameConfig Config;
    public Block[,] matrix;
    public List<Vector2> matrixList;
    public List<Vector2Int> deletedPos;
    public AroundData aroundData;
    public void Setup()
    {
        matrix = new Block[Config.GridSize.x, Config.GridSize.y];
        CreateHexGround();
    }
    public void CreateHexGround()
    {
        var grid = GetComponent<Grid>();
        
        for (var j = 0; j < Config.GridSize.y; j++)
        {
            var height = Config.GridSize.x + (j % 2 == 0 ? 0 : -1);
            for (var i = 0; i < height; i++)
            {
                CellBackGroundCreate(grid, i, j);
            }
        }
        
        //12 - 4, 13 -4.5 , 14 - 5
        var x = (Screen.width / 2);
        var y = (Screen.height / 2);
        
        var widthCam = (Config.GridSize.y * 0.75f) / 2;
        var heightCam = (Config.GridSize.x * 0.8659766f) / 2;
        
        cam.transform.position = new Vector3(widthCam-0.375f,heightCam,-20);
        
        var wantedWidthCamDist = Config.GridSize.y / 2f;
        Camera.main.orthographicSize = wantedWidthCamDist * (float)Screen.height / Screen.width;
    }
    
    private void CellBackGroundCreate(Grid grid, int x, int y)
    {
        var pos = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
        var hexCell = Instantiate(cell, pos, Quaternion.identity);
        hexCell.transform.SetParent(cellBase.transform);
    }

    public List<Block> FindAllNearSameValue(Block block)
    {
        var toSearch = new List<Block>();
        var searched = new List<Block>();
        var allSameBlocks = new List<Block>();
        toSearch.Add(block);
        allSameBlocks.Add(block);
        var tempCount = 0;
        while (!toSearch.IsNullOrEmpty())
        {
            var currSearchTarget = toSearch[0];
            var sameBlocks = FindNearSameValue(currSearchTarget);
            if (!sameBlocks.IsNullOrEmpty())
            {
                for (var i = 0; i < sameBlocks.Count; i++)
                {
                    var currSameBlock = sameBlocks[i];
                    if (!searched.Contains(currSameBlock) && !toSearch.Contains(currSameBlock))
                    {
                        allSameBlocks.Add(currSameBlock);
                        toSearch.Add(currSameBlock);
                    }
                }
            }
            searched.Add(currSearchTarget);
            toSearch.Remove(currSearchTarget);

            if(500 < tempCount) break;
            tempCount++;
        }
        return allSameBlocks;
    }
    
    public List<Block> FindNearSameValue(Block block)
    {
        List<Block> sameBlockList = new List<Block>();
        for (int i = 0; i < aroundData.around.Count; i++)
        {
            if (block.Coord.y % 2 == 0)
            {
                var neibor = block.Coord + aroundData.around[i].aroundOneCoord;
                if (Boundary(neibor))
                {
                    var neiborValue = matrix[neibor.x, neibor.y];
                    if (neiborValue != null && neiborValue.value == block.value)
                    {
                        sameBlockList.Add(matrix[neibor.x,neibor.y]);
                    }
                }
            }
            else
            {
                var neibor = block.Coord + aroundData.around[i].aroundTwoCoord;
                if (Boundary(neibor) && matrix[neibor.x,neibor.y] != null)
                {
                    var neiborValue = matrix[neibor.x, neibor.y];
                    if (neiborValue != null && neiborValue.value == block.value)
                    {
                        sameBlockList.Add(matrix[neibor.x,neibor.y]);
                    }
                }
                
            }
        }
        return sameBlockList;
    }
    public bool Boundary(Vector2Int pos)
    {
        var width = Config.GridSize.x;
        var height = Config.GridSize.y;
        if((width % 2 == 0 && height % 2 == 0) || (width % 2 ==1 && height % 2 == 0))
        {
            if (Config.GridSize.y % 2 == 0)
            {
                if (pos.x < 0 || pos.x >= width || pos.y >= height || pos.y < 0)
                {
                    return false;
                }
            }
            else
            {
                if (pos.x < 0 || pos.x >= width-1 || pos.y >= height || pos.y < 0)
                {
                    return false;
                }
            }
        }
        else
        {
            if (Config.GridSize.y % 2 == 0)
            {
                if (pos.x < 0 || pos.x >= width-1 || pos.y >= height || pos.y < 0)
                {
                    return false;
                }
            }
            else
            {
                if (pos.x < 0 || pos.x >= width || pos.y >= height || pos.y < 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
}
