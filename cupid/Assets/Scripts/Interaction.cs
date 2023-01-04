    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] public GameObject cell;
    [SerializeField] public GameObject Cells;
    [SerializeField] private GameObject cam;
    void Start()
    {
        var grid = GetComponent<Grid>();
        
        for (int j = 0; j < 7; j++)
        {
            if (j % 2 == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    var pos = grid.GetCellCenterWorld(new Vector3Int(i, j, 0));
                    var hexCell = Instantiate(cell, pos, Quaternion.identity);
                    hexCell.transform.SetParent(Cells.transform);
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    var pos = grid.GetCellCenterWorld(new Vector3Int(i, j, 0));
                    var hexCell = Instantiate(cell, pos, Quaternion.identity);
                    hexCell.transform.SetParent(Cells.transform);
                }
            }
            
            
        }
        cam.transform.position = new Vector3((float)8 / 4f, (float)8 / 4f, -20);
    }

    private void Update()
    {
        var plane = new Plane();
        plane.Set3Points(Vector3.zero, Vector3.up, Vector3.right);
        
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out var enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                
                var grid = GetComponent<Grid>();
                var cellCoord = grid.WorldToCell(hitPoint);
                Debug.Log(cellCoord);
                
            }
                
        }
    }
}
