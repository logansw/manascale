using UnityEngine;

public class PlayerTurnState : State
{
    public override void OnEnter()
    {
        ManaManager.InstancePlayer.AdvanceWhiteMana();
        ManaManager.InstancePlayer.CheckAlive();
        DeckManager.Instance.DrawNewHand();
        BattleManager.Instance.ToggleContinueButton(true);
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        ManaManager.InstancePlayer.AdvanceRedMana();
        ManaManager.InstanceEnemy.CheckAlive();
    }
}