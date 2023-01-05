using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wayway.Engine.Singleton;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private SpawnAndDelete spawnAndDelete;
    [SerializeField] private Interaction interaction;
    
    public States State { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        State = States.ReadyForInteraction;
        Map.Instance.CreateHexGround();
        spawnAndDelete.SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(State);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            var plane = new Plane();
            plane.Set3Points(Vector3.zero, Vector3.up, Vector3.right);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out var enter))
            {
                
                Vector3 hitPoint = ray.GetPoint(enter);
        
                var grid = Map.Instance.GetComponent<Grid>();
                var cellCoord = grid.WorldToCell(hitPoint);
                var clickedBlock = Map.Instance.matrix[cellCoord.x, cellCoord.y];
                Debug.Log(cellCoord);
                Debug.Log(Map.Instance.matrix[cellCoord.x, cellCoord.y]);

            }
        }
        
        switch (State)
        {
            case States.ReadyForInteraction:
                interaction.ClickForMerge();
                
                break;
            case States.DeleteBlock:
                interaction.DeleteMergedObj(interaction.sameBlocks);
                Debug.Log("delete");
                State = States.CreateNewBlock;
                
                break;
            case States.CreateNewBlock:
                spawnAndDelete.CreateNewBlockForEmptyPlaceAndCheckTarget();
                Debug.Log("create");
                State = States.CheckTarget;
                break;
            case States.CheckTarget:
                spawnAndDelete.CheckTarget();
                Debug.Log("check");
                State = States.DownNewBlock;
                break;
            case States.DownNewBlock:
                spawnAndDelete.MoveAllBlocks();
                State = States.Waiting;
                break;
            case States.Waiting:
                //Debug.Log("기달리는중");
                break;
        }
    }
    public void ChangeState(States stateType)
    {
        State = stateType;
    }
}

public enum States
{
    ReadyForInteraction,
    CheckTarget,
    Waiting,
    DeleteBlock,
    CreateNewBlock,
    DownNewBlock,
}
