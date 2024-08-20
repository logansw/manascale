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
        List<Mana> toDestroy = SpellManager.Instance.SelectedManaEnemy;
        int red = 0;
        int blue = 0;
        int white = 0;
        foreach (Mana mana in SpellManager.Instance.SelectedManaEnemy)
        {
            if (mana.Type == ManaType.Red)
            {
                red++;
            }
            else if (mana.Type == ManaType.Blue)
            {
                blue++;
            }
            else if (mana.Type == ManaType.White)
            {
                white++;
            }
        }
        ManaManager.InstanceEnemy.ChangeMana(ManaType.Red, -red);
        ManaManager.InstanceEnemy.ChangeMana(ManaType.Blue, -blue);
        ManaManager.InstanceEnemy.ChangeMana(ManaType.White, -white);
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