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
        
        switch (State)
        {
            case States.ReadyForInteraction:
                interaction.ClickForMerge();
                break;
            case States.MergeBlock:
                
                break;
            case States.DeleteBlock:
                interaction.DeleteMergedObj(interaction.sameBlocks);
                State = States.CreateNewBlock;
                break;
            case States.CreateNewBlock:
                spawnAndDelete.CreateNewBlockForEmptyPlace();
                State = States.DownNewBlock;
                break;
            case States.DownNewBlock:

                State = States.ReadyForInteraction;
                break;
            case States.FindWhatCanMerge:
                
                break;
        }
    }
    
    // public bool IsThereMovingBlock
    // {
    //     get
    //     {
    //         for (int i = 0; i < Map.Width; i++)
    //         {
    //             for (int j = 0; j < Map.Height; j++)
    //             {
    //                 var currPang = Map.Matrix[i,j];
    //                 if (currPang.isMoving)
    //                 {
    //                     return true;
    //                 }
    //             }
    //         }
    //         return false;
    //     }
    // }
    

    public void ChangeState(States stateType)
    {
        State = stateType;
    }
}

public enum States
{
    ReadyForInteraction,
    FindWhatCanMerge,
    MergeBlock,
    DeleteBlock,
    CreateNewBlock,
    DownNewBlock,
}
