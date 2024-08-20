using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<EnemySpellSO> HighBalanceSpells;
    public List<EnemySpellSO> MediumBalanceSpells;
    public List<EnemySpellSO> LowBalanceSpells;
    public EnemySpellSO IntendedSpell;
    private int _lowerThirdThreshhold;
    private int _upperThirdThreshhold;
    public EnemyIntent Intent;

    public void Initialize(EnemyIntent intent)
    {
        Intent = intent;
        ChooseSpell();
    }

    public void CalculateThreshholds()
    {
        _lowerThirdThreshhold = ManaManager.InstanceEnemy.GetAvailableMana() / 3;
        _upperThirdThreshhold = ManaManager.InstanceEnemy.GetAvailableMana() - _lowerThirdThreshhold - 1;
    }

    public void ChooseSpell()
    {
        CalculateThreshholds();
        List<EnemySpellSO> spellSet;
        ManaManager enemyMana = ManaManager.InstanceEnemy;
        if (enemyMana.Blue < _lowerThirdThreshhold)
        {
            spellSet = LowBalanceSpells;
        }
        else if (enemyMana.Blue > _upperThirdThreshhold)
        {
            spellSet = HighBalanceSpells;
        }
        else
        {
            spellSet = MediumBalanceSpells;
        }
        
        List<EnemySpellSO> castableSpells = new List<EnemySpellSO>();
        foreach (EnemySpellSO spell in spellSet)
        {
            if (spell.CanCast())
            {
                castableSpells.Add(spell);
            }
        }
        int index = Random.Range(0, castableSpells.Count);
        if (castableSpells.Count == 0)
        {
            IntendedSpell = null;
            Intent.RenderIntent(null);
        }
        else
        {
            IntendedSpell = castableSpells[Random.Range(0, castableSpells.Count)];
            Intent.RenderIntent(IntendedSpell);
        }
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