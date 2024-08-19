using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWhiteReversal : Spell
{
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(1, 10), new ManaRange(-1, -1));
    }

    public override bool CanCast()
    {
        foreach (Mana mana in SpellManager.Instance.SelectedManaPlayer)
        {
            if (mana.Type != ManaType.White)
            {
                return false;
            }
        }
        return true;
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
        return "Send any number of White from self to enemy";
    }

    public override string GetName()
    {
        return "WhiteReversal";
    }
}