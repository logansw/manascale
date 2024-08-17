using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(0, 0), new ManaRange(-1, -1));
    }

    public override bool CanCast()
    {
        return ManaManager.InstancePlayer.GetMana(ManaType.Blue) > 0;
    }

    public override void Cast()
    {
        ManaManager.InstancePlayer.ChangeMana(ManaType.Blue, -1);
        ManaManager.InstancePlayer.ChangeMana(ManaType.Red, 1);
    }

    public override string GetDescription()
    {
        return "Convert 1 blue mana to 1 red mana";
    }

    public override string GetName()
    {
        return "Red";
    }
}