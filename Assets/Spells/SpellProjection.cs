using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjection : Spell
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
        ManaManager.InstanceEnemy.ReceiveMana(manaType, 1);
    }

    public override string GetDescription()
    {
        return "Send 1 mana of choice to enemy";
    }

    public override string GetName()
    {
        return "Projection";
    }
}