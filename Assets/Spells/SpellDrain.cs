using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDrain : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(-1, -1), new ManaRange(0, 2));
    }

    public override bool CanCast()
    {
        return ManaManager.InstancePlayer.Red > 0 && SpellManager.Instance.SelectedManaEnemy.Count <= 2;
    }
    public override void Cast()
    {
        ManaManager.InstancePlayer.ChangeMana(ManaType.Red, -1);
        foreach (Mana mana in SpellManager.Instance.SelectedManaEnemy)
        {
            ManaManager.InstancePlayer.ChangeMana(mana.Type, 1);
            ManaManager.InstanceEnemy.ChangeMana(mana.Type, -1);
        }
    }

    public override string GetDescription()
    {
        return "Spend 1 Red to steal up to 2 mana from the enemy";
    }

    public override string GetName()
    {
        return "Drain";
    }
}