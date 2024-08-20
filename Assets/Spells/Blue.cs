using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(-1, -1), new ManaRange(-1, -1));
    }

    public override bool CanCast()
    {
        return true;
    }
    public override void Cast()
    {
        ManaManager.InstancePlayer.ChangeMana(ManaType.Blue, 1);
    }

    public override string GetDescription()
    {
        return "Create 1 blue mana";
    }

    public override string GetName()
    {
        return "Blue";
    }
}