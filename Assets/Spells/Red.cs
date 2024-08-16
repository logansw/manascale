using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : Card
{
    public override bool CanCast()
    {
        return ManaManager.Instance.GetMana(ManaType.Blue) > 0;
    }

    public override void Cast()
    {
        ManaManager.Instance.ChangeMana(ManaType.Blue, -1);
        ManaManager.Instance.ChangeMana(ManaType.Red, 1);
    }

    public override string GetDescription()
    {
        return "Convert 1 blue mana to 1 red mana";
    }

    public override string GetName()
    {
        return "Red";
    }
}