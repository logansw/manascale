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
    [SerializeField] private Button _castButton;

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
        UpdateCastButton();
        BattleManager.Instance.ToggleContinueButton(false);
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
        UpdateCastButton();
        BattleManager.Instance.ToggleContinueButton(true);
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