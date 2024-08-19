using UnityEngine;

public class PlayerTurnState : State
{
    public override void OnEnter()
    {
        ManaManager.InstancePlayer.AdvanceWhiteMana();
        ManaManager.InstancePlayer.CheckAlive();
        DeckManager.Instance.DrawCards(2);
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