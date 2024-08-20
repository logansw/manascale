using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRemove : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(0, 1), new ManaRange(-1, -1));
    }

    public override bool CanCast()
    {
        return SpellManager.Instance.SelectedManaPlayer.Count == 1;
    }

    public override void Cast()
    {
        ManaType manaType = SpellManager.Instance.SelectedManaPlayer[0].Type;
        ManaManager.InstancePlayer.ChangeMana(manaType, -1);
    }

    public override string GetDescription()
    {
        return "Remove 1 mana from self";
    }

    public override string GetName()
    {
        return "Remove";
    }
}