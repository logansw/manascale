using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private EnemyIntent _enemyIntent;
    public static BattleManager Instance;
    public Enemy CurrentEnemy;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CurrentEnemy.ChooseSpell();
        _enemyIntent.RenderIntent(CurrentEnemy.IntendedSpell);
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
            _enemyIntent.RenderIntent(CurrentEnemy.IntendedSpell);
        }
    }

    void ShowVictory()
    {
        Debug.Log("Victory!");
    }

    void ShowDefeat()
    {
        Debug.Log("Defeat.");
    }
}
