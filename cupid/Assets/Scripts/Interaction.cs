    using System;
using System.Collections;
using System.Collections.Generic;
    using DG.Tweening;
    using TMPro;
    using Unity.VisualScripting;
    using UnityEngine;

    public class Interaction : MonoBehaviour
{
    [SerializeField] private SpawnAndDelete spawn;
    //for calculate click number because create 4 block after 20times click
    private int clickCounter = 0;
    public List<Block> sameBlocks;
    public List<Vector3> posForCreate;
    
    public void ClickForMerge()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sameBlocks = new List<Block>();
            var plane = new Plane();
            plane.Set3Points(Vector3.zero, Vector3.up, Vector3.right);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out var enter))
            {
                //Debug.Log(enter);
                Vector3 hitPoint = ray.GetPoint(enter);
                
                //Debug.Log(hitPoint);
                var grid = Map.Instance.GetComponent<Grid>();
                var cellCoord = grid.WorldToCell(hitPoint);
                Debug.Log(cellCoord);
                if (Map.Instance.Boundary(new Vector2Int(cellCoord.x, cellCoord.y)))
                {
                    var clickedBlock = Map.Instance.matrix[cellCoord.x, cellCoord.y];
                    //Debug.Log(cellCoord);
                    //Debug.Log(Map.Instance.matrix[cellCoord.x, cellCoord.y].Coord);
                    var targetPos = grid.GetCellCenterWorld(cellCoord);
                    sameBlocks = Map.Instance.FindAllNearSameValue(clickedBlock);
                    
                    if (sameBlocks.Count > 1)
                    {
                        GameManager.Instance.State = States.Waiting;
                        Merge(sameBlocks,targetPos);
                        clickCounter++;
                    }
                    else
                    {
                        Debug.Log("합칠수없는곳 선택함");
                        Debug.Log(sameBlocks.Count);
                    }
                }
            }
        }
    }
    public void Merge(List<Block> allSameBlock,Vector3 target)
    {
        var sequenceAnim = DOTween.Sequence();
        for (int i = 0; i < allSameBlock.Count; i++)
        {
            sequenceAnim.Join(allSameBlock[i].transform.DOMove(target, 0.5f).SetEase(Ease.OutCubic));
            //Debug.Log(allSameBlock[i].Coord);
        }
        
        sequenceAnim.OnComplete(() => GameManager.Instance.ChangeState(States.DeleteBlock));
        //Debug.Log(allSameBlock.Count + " 머지 zkdnsxm");
    }
    public void DeleteMergedObj(List<Block> allSameBlock)
    {
        Map.Instance.deletedPos = new List<Vector2Int>();
        var clickedBlockValue = allSameBlock[0].score;
        //Debug.Log(allSameBlock.Count);
        for (int i = allSameBlock.Count-1; i > 0; i--)
        {
            Destroy(Map.Instance.matrix[allSameBlock[i].Coord.x, allSameBlock[i].Coord.y].gameObject);
            spawn.allBlock.Remove(Map.Instance.matrix[allSameBlock[i].Coord.x, allSameBlock[i].Coord.y]);
            Map.Instance.deletedPos.Add(new Vector2Int(allSameBlock[i].Coord.x, allSameBlock[i].Coord.y));
            clickedBlockValue += allSameBlock[i].score;
            //Debug.Log(allSameBlock[i].score + "이거는" + i + "얘꺼");
        }
        allSameBlock[0].GetComponentInChildren<TextMeshPro>().text = clickedBlockValue.ToString();
        allSameBlock[0].score = clickedBlockValue;
        //Debug.Log(Map.Instance.deletedPos.Count);
    }
}
