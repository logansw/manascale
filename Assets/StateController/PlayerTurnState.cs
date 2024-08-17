using UnityEngine;

public class PlayerTurnState : State
{
    public override void OnEnter()
    {
        ManaManager.InstancePlayer.AdvanceWhiteMana();
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        ManaManager.InstancePlayer.AdvanceRedMana();
    }
}