    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    
    //for calculate click number because create 4 block after 20times click
    private int clickCounter;
    
    public void FindWhereDidClick()
    {
        var plane = new Plane();
        plane.Set3Points(Vector3.zero, Vector3.up, Vector3.right);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out var enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            
            var grid = Map.Instance.GetComponent<Grid>();
            var cellCoord = grid.WorldToCell(hitPoint);
            Debug.Log(cellCoord);
            Debug.Log(Map.Instance.matrix[cellCoord.x,cellCoord.y]);
        }
    }
}
