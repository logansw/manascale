using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : Singleton<StateController>
{
    public State CurrentState;
    private PlayerTurnState _playerTurnState;
    private EnemyTurnState _enemyTurnState;
    private ShopState _shopState;

    protected override void Awake()
    {
        base.Awake();
        _playerTurnState = gameObject.AddComponent<PlayerTurnState>();
        _enemyTurnState = gameObject.AddComponent<EnemyTurnState>();
        _shopState = gameObject.AddComponent<ShopState>();
    }

    public override void Initialize()
    {
        ChangeState(StateType.PlayerTurn);
    }

    void OnEnable()
    {
        BattleManager.e_OnVictory += OnVictory;
    }

    void OnDisable()
    {
        BattleManager.e_OnVictory -= OnVictory;
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
            case StateType.Shop:
                CurrentState = _shopState;
                break;
        }

        CurrentState?.OnEnter();
    }

    private void OnVictory()
    {
        ChangeState(StateType.Shop);
    }
}

public enum StateType
{
    None,
    PlayerTurn,
    EnemyTurn,
    Shop,
}