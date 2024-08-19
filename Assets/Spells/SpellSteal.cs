using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSteal : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(-1, -1), new ManaRange(1, 1));
    }

    public override bool CanCast()
    {
        return SpellManager.Instance.SelectedManaEnemy.Count == 1;
    }

    public override void Cast()
    {
        ManaType manaType = SpellManager.Instance.SelectedManaEnemy[0].Type;
        ManaManager.InstancePlayer.ChangeMana(manaType, 1);
        ManaManager.InstanceEnemy.ChangeMana(manaType, -1);
    }

    public override string GetDescription()
    {
        return "Steal 1 mana from enemy";
    }

    public override string GetName()
    {
        return "Steal";
    }
}