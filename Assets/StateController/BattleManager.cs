using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
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
    }
}
