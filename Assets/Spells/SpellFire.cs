using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFire : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(-1, -1), new ManaRange(-1, -1));
    }

    public override bool CanCast()
    {
        return ManaManager.InstancePlayer.GetMana(ManaType.Red) > 0;
    }

    public override void Cast()
    {
        ManaManager.InstancePlayer.ChangeMana(ManaType.Red, -1);
        ManaManager.InstanceEnemy.ReceiveMana(ManaType.Red, 2);
    }

    public override string GetDescription()
    {
        return "Send 2 red to enemy. Costs 1 red.";
    }

    public override string GetName()
    {
        return "Fire";
    }
}