using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TargetingRules
{
    public ManaRange SelfManaRange;
    public ManaRange EnemyManaRange;
    public bool CanTargetSelf => SelfManaRange.MaximumMana >= 0;
    public bool CanTargetEnemies => EnemyManaRange.MaximumMana >= 0;

    public TargetingRules(ManaRange selfManaRange, ManaRange enemyManaRange)
    {
        SelfManaRange = selfManaRange;
        EnemyManaRange = enemyManaRange;
    }
}

public struct ManaRange
{
    public int MinimumMana;
    public int MaximumMana;

    public ManaRange(int minimumMana, int maximumMana)
    {
        MinimumMana = minimumMana;
        MaximumMana = maximumMana;
    }
}