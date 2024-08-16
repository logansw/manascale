using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : Card
{
    public override bool CanCast()
    {
        return true;
    }
    public override void Cast()
    {
        ManaManager.Instance.ChangeMana(ManaType.Blue, 1);
    }

    public override string GetDescription()
    {
        return "Create 1 blue mana";
    }

    public override string GetName()
    {
        return "Blue";
    }
}