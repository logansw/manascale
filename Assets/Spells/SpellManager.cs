using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Instance;
    public Spell SelectedSpell;
    public SpellSelectState SpellSelectState;
    public List<Mana> SelectedManaPlayer;
    public List<Mana> SelectedManaEnemy;
    public Button CastButton;

    void Awake()
    {
        Instance = this;
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
        spell.transform.position += new Vector3(0, 100, 0);
    }

    public void DeselectSpell()
    {
        if (SelectedSpell != null)
        {
            SelectedSpell.transform.position += new Vector3(0, -100, 0);
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
}

public enum SpellSelectState
{
    None,
    ChoosingTarget
}