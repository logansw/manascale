using UnityEngine;

public class EnemyTurnState : State
{
    public override void OnEnter()
    {
        ManaManager.InstanceEnemy.AdvanceWhiteMana();
        BattleManager.Instance.CurrentEnemy.TryCast();
        BattleManager.Instance.Continue();
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        ManaManager.InstanceEnemy.AdvanceRedMana();
        BattleManager.Instance.CurrentEnemy.ChooseSpell();
    }
}