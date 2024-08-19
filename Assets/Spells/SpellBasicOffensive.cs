using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBasicOffensive : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(-1, -1), new ManaRange(-1, -1));
    }

    public override bool CanCast()
    {
        return ManaManager.InstancePlayer.GetMana(ManaType.Blue) > 0;
    }

    public override void Cast()
    {
        ManaManager.InstancePlayer.ChangeMana(ManaType.Blue, -1);
        ManaManager.InstanceEnemy.ReceiveMana(ManaType.Red, 1);
    }

    public override string GetDescription()
    {
        return "Send 1 red to enemy. Costs 1 blue.";
    }

    public override string GetName()
    {
        return "Basic Offensive Magic";
    }
}