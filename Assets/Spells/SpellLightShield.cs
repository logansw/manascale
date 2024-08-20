using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellLightShield : Spell
{
    protected override void Start()
    {
        base.Start();
        ExhaustAfterUse = true;
    }
    
    public override TargetingRules GetTargetingRules()
    {
        return new TargetingRules(new ManaRange(-1, -1), new ManaRange(-1, -1));
    }

    public override bool CanCast()
    {
        return true;
    }
    public override void Cast()
    {
        ManaManager.InstancePlayer.ChangeMana(ManaType.White, 2);
    }

    public override string GetDescription()
    {
        return "Create 2 white mana. Exhaust.";
    }

    public override string GetName()
    {
        return "Light Shield";
    }
}