using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMemory : Spell
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
        DeckManager.Instance.DrawCards(2);
    }

    public override string GetDescription()
    {
        return "Convert 1 Blue to 2 cards";
    }

    public override string GetName()
    {
        return "Memory";
    }
}