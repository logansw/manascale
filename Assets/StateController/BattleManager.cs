using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : Singleton<BattleManager>
{
    public Enemy CurrentEnemy;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _castButton;
    public static Action e_OnVictory;
    public static Action e_OnDefeat;

    public override void Initialize()
    {
        // Do nothing
    }

    public void Reset()
    {
        DeckManager.Instance.Reset();
        StateController.Instance.ChangeState(StateType.PlayerTurn);
    }

    void OnEnable()
    {
        ManaManager.InstancePlayer.e_OnEmpty += ShowDefeat;
        ManaManager.InstancePlayer.e_OnOverflow += ShowDefeat;
        ManaManager.InstanceEnemy.e_OnEmpty += ShowVictory;
        ManaManager.InstanceEnemy.e_OnOverflow += ShowVictory;
    }

    void OnDisable()
    {
        ManaManager.InstancePlayer.e_OnEmpty -= ShowDefeat;
        ManaManager.InstancePlayer.e_OnOverflow -= ShowDefeat;
        ManaManager.InstanceEnemy.e_OnEmpty -= ShowVictory;
        ManaManager.InstanceEnemy.e_OnOverflow -= ShowVictory;
    }

    public void Continue()
    {
        if (StateController.Instance.CurrentState == null)
        {
            StateController.Instance.ChangeState(StateType.PlayerTurn);
        }
        else if (StateController.Instance.CurrentState is PlayerTurnState)
        {
            StateController.Instance.ChangeState(StateType.EnemyTurn);
        }
        else if (StateController.Instance.CurrentState is EnemyTurnState)
        {
            StateController.Instance.ChangeState(StateType.PlayerTurn);
        }
        else if (StateController.Instance.CurrentState is ShopState)
        {
            StateController.Instance.ChangeState(StateType.PlayerTurn);
        }
    }

    public void ToggleContinueButton(bool interactable)
    {
        _continueButton.interactable = interactable;
    }

    public void ToggleCastButton(bool interactable)
    {
        _castButton.interactable = interactable;
    }

    void ShowVictory()
    {
        e_OnVictory?.Invoke();
    }

    void ShowDefeat()
    {
        e_OnDefeat?.Invoke();
    }
}
