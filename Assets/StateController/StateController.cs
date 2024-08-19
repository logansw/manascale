using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public static StateController Instance;
    public State CurrentState;
    private PlayerTurnState _playerTurnState;
    private EnemyTurnState _enemyTurnState;

    void Awake()
    {
        Instance = this;
        _playerTurnState = gameObject.AddComponent<PlayerTurnState>();
        _enemyTurnState = gameObject.AddComponent<EnemyTurnState>();
    }

    void Start()
    {
        ChangeState(StateType.PlayerTurn);
    }

    public void ChangeState(StateType stateType)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }

        switch (stateType)
        {
            case StateType.None:
                CurrentState = null;
                break;
            case StateType.PlayerTurn:
                CurrentState = _playerTurnState;
                break;
            case StateType.EnemyTurn:
                CurrentState = _enemyTurnState;
                break;
        }

        CurrentState?.OnEnter();
    }
}

public enum StateType
{
    None,
    PlayerTurn,
    EnemyTurn,
}