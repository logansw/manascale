using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFocus : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(-1, -1), new ManaRange(-1, -1));
    }

    public override bool CanCast()
    {
        return ManaManager.InstancePlayer.GetFunctionalMana() < ManaManager.InstancePlayer.GetAvailableMana();
    }

    public override void Cast()
    {
        ManaManager.InstancePlayer.ChangeMana(ManaType.Blue, 1);
        DeckManager.Instance.DrawCards(1);
    }

    public override string GetDescription()
    {
        return "Gain 1 Blue and Draw 1 card";
    }

    public override string GetName()
    {
        return "Focus";
    }
}