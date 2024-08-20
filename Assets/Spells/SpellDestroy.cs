using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDestroy : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(-1, -1), new ManaRange(0, 2));
    }

    public override bool CanCast()
    {
        return ManaManager.InstancePlayer.Blue >= 2 && SpellManager.Instance.SelectedManaEnemy.Count <= 2;
    }
    public override void Cast()
    {
        ManaManager.InstancePlayer.ChangeMana(ManaType.Blue, -2);
        foreach (Mana mana in SpellManager.Instance.SelectedManaEnemy)
        {
            ManaManager.InstanceEnemy.ChangeMana(mana.Type, -1);
        }
    }

    public override string GetDescription()
    {
        return "Spend 2 Blue to destroy up to 2 mana from enemy";
    }

    public override string GetName()
    {
        return "Destroy";
    }
}