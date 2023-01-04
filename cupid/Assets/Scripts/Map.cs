using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wayway.Engine.Singleton;

public class Map : MonoSingleton<Map>
{
    [SerializeField] public GameObject cell;
    [SerializeField] public GameObject cellBase;
    [SerializeField] private GameObject cam;
    public Block[,] matrix = new Block[7,7];
    //AroundPosition six, Scriptable object
    public AroundData aroundData;

    
    private void Start()
    {
        Debug.Log(aroundData.around[0].aroundOneCoord);
    }

    
    //배경 생성후 좌표 저장
    public void CreateHexGround()
    {
        var grid = GetComponent<Grid>();
        for (var j = 0; j < 7; j++)
        {
            if (j % 2 == 0)
            {
                for (var i = 0; i < 7; i++)
                {
                    CellBackGroundCreate(grid, i, j);
                }
            }
            else
            {
                for (var i = 0; i < 6; i++)
                {
                    CellBackGroundCreate(grid, i, j);
                }
            }
        }
        cam.transform.position = new Vector3((float)8 / 4f, (float)8 / 4f, -20);
    }
    
    private void CellBackGroundCreate(Grid grid, int x, int y)
    {
        var pos = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
        var hexCell = Instantiate(cell, pos, Quaternion.identity);
        hexCell.transform.SetParent(cellBase.transform);
    }

    
    
}
