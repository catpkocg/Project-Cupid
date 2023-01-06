    using System;
using System.Collections;
using System.Collections.Generic;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;

    public class Interaction : MonoBehaviour
{
    [SerializeField] private SpawnAndDelete spawn;


    public Vector2Int catfootPos;
    public Vector2Int catkingPos;
    
    private List<Block> deletePalce;
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
                Vector3 hitPoint = ray.GetPoint(enter);
                var grid = Map.Instance.GetComponent<Grid>();
                var cellCoord = grid.WorldToCell(hitPoint);
                if (Map.Instance.Boundary(new Vector2Int(cellCoord.x, cellCoord.y)))
                {
                    var clickedBlock = Map.Instance.matrix[cellCoord.x, cellCoord.y];
                    var targetPos = grid.GetCellCenterWorld(cellCoord);
                    sameBlocks = Map.Instance.FindAllNearSameValue(clickedBlock);

                    if (clickedBlock.value != 100)
                    {
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
                    else
                    {
                        Debug.Log("옮길수없음");
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
        }
        
        sequenceAnim.OnComplete(() => GameManager.Instance.ChangeState(States.DeleteBlock));
    }
    public void DeleteMergedObj(List<Block> allSameBlock)
    {
        deletePalce = new List<Block>();
        Map.Instance.deletedPos = new List<Vector2Int>();
        var clickedBlockValue = allSameBlock[0].score;
        if (allSameBlock[0].value == 99)
        {
            for (int i = allSameBlock.Count-1; i > 0; i--)
            {
                Destroy(Map.Instance.matrix[allSameBlock[i].Coord.x, allSameBlock[i].Coord.y].gameObject);
                spawn.allBlock.Remove(Map.Instance.matrix[allSameBlock[i].Coord.x, allSameBlock[i].Coord.y]);
                Map.Instance.deletedPos.Add(new Vector2Int(allSameBlock[i].Coord.x, allSameBlock[i].Coord.y));
                clickedBlockValue += allSameBlock[i].score;
            }
            
            catkingPos = allSameBlock[0].Coord;
            Destroy(Map.Instance.matrix[allSameBlock[0].Coord.x, allSameBlock[0].Coord.y].gameObject);
            spawn.allBlock.Remove(Map.Instance.matrix[allSameBlock[0].Coord.x, allSameBlock[0].Coord.y]);
                
            spawn.SpawnKingBlock(catkingPos,allSameBlock);
            GameManager.Instance.score += allSameBlock.Count;
        }
        else
        {
            for (int i = allSameBlock.Count-1; i > 0; i--)
            {
                Destroy(Map.Instance.matrix[allSameBlock[i].Coord.x, allSameBlock[i].Coord.y].gameObject);
                spawn.allBlock.Remove(Map.Instance.matrix[allSameBlock[i].Coord.x, allSameBlock[i].Coord.y]);
                Map.Instance.deletedPos.Add(new Vector2Int(allSameBlock[i].Coord.x, allSameBlock[i].Coord.y));
                clickedBlockValue += allSameBlock[i].score;
            }
        
            if (clickedBlockValue > Map.Instance.Config.AddMaxCount)
            {
                catfootPos = allSameBlock[0].Coord;
                Destroy(Map.Instance.matrix[allSameBlock[0].Coord.x, allSameBlock[0].Coord.y].gameObject);
                spawn.allBlock.Remove(Map.Instance.matrix[allSameBlock[0].Coord.x, allSameBlock[0].Coord.y]);
                
                spawn.SpawnFootBlock(catfootPos);
            }
            else
            {
                allSameBlock[0].GetComponentInChildren<TextMeshPro>().text = clickedBlockValue.ToString();
                allSameBlock[0].score = clickedBlockValue;
            }
        }
        
    }
}
