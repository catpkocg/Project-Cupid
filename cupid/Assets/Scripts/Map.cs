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
    
    public Block[,] matrix = new Block[7,7];

    public List<Vector2Int> deletedPos; 
    
    //public Text label;

    public Canvas gridCanvas;

    public Text label;
    //AroundPosition six, Scriptable object
    public AroundData aroundData;

    public void Setup()
    {
        matrix = new Block[Config.GridSize.y, Config.GridSize.x];
        CreateHexGround();
    }
    //배경 생성후 좌표 저장
    public void CreateHexGround()
    {
        var grid = GetComponent<Grid>();
        
        for (var j = 0; j < Config.GridSize.x; j++)
        {
            var height = Config.GridSize.y + (j % 2 == 0 ? 0 : -1);
            for (var i = 0; i < height; i++)
            {
                CellBackGroundCreate(grid, i, j);
            }
        }

        var widthCam = Config.GridSize.x / 2 * 0.75f;
        var heightCam = Config.GridSize.y / 2 * 0.8659766f;
        cam.transform.position = new Vector3(widthCam, heightCam, -20);
        
        var wantedWidthCamDist = Config.GridSize.x / 2f;
        Camera.main.orthographicSize = wantedWidthCamDist * (float)Screen.height / Screen.width;

    }
    
    private void CellBackGroundCreate(Grid grid, int x, int y)
    {
        
        var pos = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
        var hexCell = Instantiate(cell, pos, Quaternion.identity);
        //var hexLabel = Instantiate<Text>(label, pos, Quaternion.identity);
        //hexLabel.rectTransform.SetParent(gridCanvas.transform, false);
        //hexLabel.rectTransform.anchoredPosition = new Vector2(x, y);
        hexCell.transform.SetParent(cellBase.transform);
        
    }

    public List<Block> FindAllNearSameValue(Block block)
    {
        // 검색할 것들
        // 검색한 것들
        // 연결된 것들
        var toSearch = new List<Block>();
        var searched = new List<Block>();
        var allSameBlocks = new List<Block>();

        // 검색의 시작점을 검색할 것들에 추가한다.
        toSearch.Add(block);
        allSameBlocks.Add(block);

        var tempCount = 0;
        
        // 검색할 것들이 없으면
        while (!toSearch.IsNullOrEmpty())
        {
            // 검색:
            // 검색할 것들의 첫번째 놈을 검색한다, 다음과 같이
            var currSearchTarget = toSearch[0];
            // 검색의 대상을 설정한다.
            var sameBlocks = FindNearSameValue(currSearchTarget);
            // 검색의 대상은 주변 6개 칸중 이미 연결됨을 알거나, 이미 검색했던 것을 제외한다.
            if (!sameBlocks.IsNullOrEmpty())
            {
                for (var i = 0; i < sameBlocks.Count; i++)
                {
                    var currSameBlock = sameBlocks[i];
                    if (!searched.Contains(currSameBlock) && !toSearch.Contains(currSameBlock))
                    {
                        // 연결된 것을 찾으면 연결된 것들에 추가한다.
                        // 연결된 것을 찾으면 검색할 것들에 추가한다.
                        allSameBlocks.Add(currSameBlock);
                        toSearch.Add(currSameBlock);
                    }
                }
            }
            // 현재 검색중인 것의 검색이 끝나면 검색할 것들에서 빼준다.
            searched.Add(currSearchTarget);
            toSearch.Remove(currSearchTarget);

            if(500 < tempCount) break;
            tempCount++;
        }
        // return 연결된 것들
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
                    var neiborValue = matrix[neibor.y, neibor.x];
                    if (neiborValue != null && neiborValue.value == block.value)
                    {
                        sameBlockList.Add(matrix[neibor.y,neibor.x]);
                    }
                }
            }
            else
            {
                var neibor = block.Coord + aroundData.around[i].aroundTwoCoord;
                if (Boundary(neibor))
                {
                    var neiborValue = matrix[neibor.y, neibor.x];
                    if (neiborValue != null && neiborValue.value == block.value)
                    {
                        sameBlockList.Add(matrix[neibor.y,neibor.x]);
                    }
                }
                
            }
        }
        return sameBlockList;
    }

    

    public bool Boundary(Vector2Int pos)
    {
        var width = Config.GridSize.x;
        var height = Config.GridSize.y + (pos.y % 2 == 0 ? 0 : -1);
        
        if (pos.x < 0 || pos.x > height || pos.y > width || pos.y < 0)
        {
            return false;
        }

        return true;
    }
    
}
