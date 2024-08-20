using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressionManager : Singleton<ProgressionManager>
{
    [SerializeField] TMP_Text _spellSelectionText;
    private int _spellsRemaining;
    [SerializeField] private Button _addSpellButton;

    public override void Initialize()
    {
        // Do nothing
    }

    void OnEnable()
    {
        SpellManager.e_OnSpellSelectionChanged += UpdateAddSpellButton;
        BattleManager.e_OnVictory += DisplaySpells;
    }

    void OnDisable()
    {
        SpellManager.e_OnSpellSelectionChanged -= UpdateAddSpellButton;
        BattleManager.e_OnVictory -= DisplaySpells;
    }

    public void DisplaySpells()
    {
        _spellsRemaining = 2;
        _spellSelectionText.text = $"Choose {_spellsRemaining} spells to add to your deck";
        Spell[] oldSpells = new Spell[5];
        for (int i = 0; i < DeckManager.Instance.Shop.Count; i++)
        {
            Spell spell = DeckManager.Instance.Shop[i];
            oldSpells[i] = spell;
        }
        for (int i = 0; i < oldSpells.Length; i++)
        {
            if (oldSpells[i] != null)
            {
                DeckManager.Instance.ShopToDatabase(oldSpells[i]);
            }
        }
        for (int i = 0; i < 5; i++)
        {
            Spell spell = DeckManager.Instance.GetRandomDatabaseSpell();
            Debug.Log(spell.gameObject.name);
            DeckManager.Instance.DatabaseToShop(spell);
        }
    }

    public void AddSpellToDeck()
    {
        DeckManager.Instance.ShopToDeck(SpellManager.Instance.SelectedSpell);
        _spellsRemaining--;
        _spellSelectionText.text = $"Choose {_spellsRemaining} spells to add to your deck";
    }

    private void UpdateAddSpellButton()
    {
        if (SpellManager.Instance.SelectedSpell == null || _spellsRemaining == 0)
        {
            _addSpellButton.interactable = false;
        }
        else
        {
            _addSpellButton.interactable = true;
        }
    }
}