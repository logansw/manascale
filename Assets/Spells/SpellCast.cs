using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(0, 2), new ManaRange(-1, -1));
    }

    public override bool CanCast()
    {
        return SpellManager.Instance.SelectedManaPlayer.Count <= 2;
    }
    public override void Cast()
    {
        foreach (Mana mana in SpellManager.Instance.SelectedManaPlayer)
        {
            ManaManager.InstancePlayer.ChangeMana(mana.Type, -1);
            ManaManager.InstanceEnemy.ReceiveMana(mana.Type, 1);
        }
    }

    public override string GetDescription()
    {
        return "Send up to 2 Mana";
    }

    public override string GetName()
    {
        return "Cast";
    }
}