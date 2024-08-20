using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : Singleton<SpellManager>
{
    public const int SELECTION_OFFSET = 20;
    public Spell SelectedSpell;
    public SpellSelectState SpellSelectState;
    public List<Mana> SelectedManaPlayer;
    public List<Mana> SelectedManaEnemy;
    [SerializeField] private Button _castButton;
    public static Action e_OnSpellSelectionChanged;

    public override void Initialize()
    {
        // Do nothing
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeselectSpell();
        }
    }

    public void SelectSpell(Spell spell)
    {
        if (SelectedSpell != null)
        {
            DeselectSpell();
        }
        SelectedSpell = spell;
        SpellSelectState = SpellSelectState.ChoosingTarget;
        spell.transform.position += new Vector3(0, SELECTION_OFFSET, 0);
        UpdateCastButton();
        BattleManager.Instance.ToggleContinueButton(false);
        e_OnSpellSelectionChanged?.Invoke();
    }

    public void DeselectSpell()
    {
        if (SelectedSpell != null)
        {
            SelectedSpell.transform.position += new Vector3(0, -SELECTION_OFFSET, 0);
        }
        SelectedSpell = null;
        SpellSelectState = SpellSelectState.None;
        foreach (Mana mana in SelectedManaPlayer)
        {
            mana.IsSelected = false;
            mana.Render();
        }
        foreach (Mana mana in SelectedManaEnemy)
        {
            mana.IsSelected = false;
            mana.Render();
        }
        SelectedManaPlayer.Clear();
        SelectedManaEnemy.Clear();
        UpdateCastButton();
        BattleManager.Instance.ToggleContinueButton(true);
        e_OnSpellSelectionChanged?.Invoke();
    }

    public void TryCast()
    {
        if (SelectedSpell != null)
        {
            if (SelectedSpell.TryCast())
            {
                DeselectSpell();
            }
            else
            {
                // Explain why to the player
            }
        }
    }

    public void UpdateCastButton()
    {
        _castButton.interactable = SelectedSpell != null && SelectedSpell.CanCast();
    }
}

public enum SpellSelectState
{
    None,
    ChoosingTarget
}