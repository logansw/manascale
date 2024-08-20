using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRedReversal : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(0, 10), new ManaRange(-1, -1));
    }

    public override bool CanCast()
    {
        foreach (Mana mana in SpellManager.Instance.SelectedManaPlayer)
        {
            if (mana.Type != ManaType.Red)
            {
                return false;
            }
        }
        return true;
    }

    public override void Cast()
    {
        int redCount = SpellManager.Instance.SelectedManaPlayer.Count;
        ManaManager.InstanceEnemy.ReceiveMana(ManaType.Red, redCount);
        ManaManager.InstancePlayer.ChangeMana(ManaType.Red, -redCount);
    }

    public override string GetDescription()
    {
        return "Send any number of Red from self to enemy";
    }

    public override string GetName()
    {
        return "RedReversal";
    }
}