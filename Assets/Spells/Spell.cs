using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class Spell : MonoBehaviour
{
    private TMP_Text _name;    
    private TMP_Text _description;
    public abstract string GetName();
    public abstract string GetDescription();
    private Button _button;
    public bool ExhaustAfterUse;

    public abstract TargetingRules GetTargetingRules();
    public abstract bool CanCast();
    public abstract void Cast();
    public RectTransform RectTransform;

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    protected virtual void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Select);
        _name = transform.Find("Name Text").GetComponent<TMP_Text>();
        _description = transform.Find("Description").GetComponent<TMP_Text>();
        _name.text = GetName();
        _description.text = GetDescription();
    }

    public void Select()
    {
        if (SpellManager.Instance.SelectedSpell == this)
        {
            SpellManager.Instance.DeselectSpell();
        }
        else
        {
            SpellManager.Instance.SelectSpell(this);
        }
    }

    public bool TryCast()
    {
        if (CanCast())
        {
            Cast();
            if (ExhaustAfterUse)
            {
                DeckManager.Instance.Exhaust(this);
            }
            else
            {
                DeckManager.Instance.Discard(this);
            }
            return true;
        }
        return false;
    }
}