using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wayway.Engine.Singleton;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private SpawnAndDelete spawnAndDelete;
    [SerializeField] private Interaction interaction;
    [SerializeField] private TextMeshProUGUI scoreText;
    public int score;
    
    public States State { get; set; }

    private void Start()
    {
        State = States.ReadyForInteraction;
        Map.Instance.Setup();
        spawnAndDelete.SpawnBlock();
        score = 0;
    }
   
    private void Update()
    {
        scoreText.text = score.ToString();
        switch (State)
        {
            case States.ReadyForInteraction:
                interaction.ClickForMerge();
                break;
            case States.DeleteBlock:
                interaction.DeleteMergedObj(interaction.sameBlocks);
                State = States.CreateNewBlock;
                break;
            case States.CreateNewBlock:
                spawnAndDelete.CreateNewBlockForEmptyPlaceAndCheckTarget();
                State = States.CheckTarget;
                break;
            case States.CheckTarget:
                spawnAndDelete.CheckTarget();
                State = States.DownNewBlock;
                break;
            case States.DownNewBlock:
                spawnAndDelete.MoveAllBlocks();
                State = States.Waiting;
                break;
            case States.Waiting:
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
    None = 0,
    ReadyForInteraction,
    CheckTarget,
    Waiting,
    DeleteBlock,
    CreateNewBlock,
    DownNewBlock,
}
