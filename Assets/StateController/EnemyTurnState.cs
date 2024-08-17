public class EnemyTurnState : State
{
    public override void OnEnter()
    {
        ManaManager.InstanceEnemy.AdvanceWhiteMana();
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        ManaManager.InstanceEnemy.AdvanceRedMana();
    }
}