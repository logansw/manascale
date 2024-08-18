using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<EnemySpellSO> HighPrioritySpells;
    public List<EnemySpellSO> MediumPrioritySpells;
    public List<EnemySpellSO> LowPrioritySpells;
    public EnemySpellSO IntendedSpell;

    public void ChooseSpell()
    {
        List<EnemySpellSO> castableSpells = new List<EnemySpellSO>();
        foreach (EnemySpellSO spell in HighPrioritySpells)
        {
            if (spell.CanCast())
            {
                castableSpells.Add(spell);
            }
        }
        if (castableSpells.Count > 0)
        {
            IntendedSpell = castableSpells[Random.Range(0, castableSpells.Count)];
            return;
        }
        foreach (EnemySpellSO spell in MediumPrioritySpells)
        {
            if (spell.CanCast())
            {
                castableSpells.Add(spell);
            }
        }
        if (castableSpells.Count > 0)
        {
            IntendedSpell = castableSpells[Random.Range(0, castableSpells.Count)];
            return;
        }
        foreach (EnemySpellSO spell in LowPrioritySpells)
        {
            if (spell.CanCast())
            {
                castableSpells.Add(spell);
            }
        }
        IntendedSpell = castableSpells[Random.Range(0, castableSpells.Count)];
        return;
    }

    public bool TryCast()
    {
        if (IntendedSpell.CanCast())
        {
            CastSpell();
            return true;
        }
        return false;
    }

    public void CastSpell()
    {
        ManaManager enemyMana = ManaManager.InstanceEnemy;
        enemyMana.ChangeMana(ManaType.Blue, -IntendedSpell.BlueCost);
        enemyMana.ChangeMana(ManaType.Red, -IntendedSpell.RedCost);
        enemyMana.ChangeMana(ManaType.Blue, IntendedSpell.BlueGenerate);
        enemyMana.ChangeMana(ManaType.White, IntendedSpell.WhiteGenerate);
        ManaManager playerMana = ManaManager.InstancePlayer;
        playerMana.ReceiveMana(ManaType.Blue, IntendedSpell.BlueSend);
        playerMana.ReceiveMana(ManaType.White, IntendedSpell.WhiteSend);
        playerMana.ReceiveMana(ManaType.Red, IntendedSpell.RedSend);
        playerMana.ReceiveMana(ManaType.Black, IntendedSpell.BlackSend);
    }
}