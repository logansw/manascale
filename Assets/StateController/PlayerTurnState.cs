using UnityEngine;

public class PlayerTurnState : State
{
    public override void OnEnter()
    {
        ManaManager.InstancePlayer.AdvanceWhiteMana();
        ManaManager.InstancePlayer.CheckAlive();
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