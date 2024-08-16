using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class Card : MonoBehaviour
{
    private TMP_Text _name;    
    private TMP_Text _description;
    public abstract string GetName();
    public abstract string GetDescription();
    public abstract bool CanCast();
    public abstract void Cast();
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TryCast);
        _name = transform.Find("Name Text").GetComponent<TMP_Text>();
        _description = transform.Find("Description").GetComponent<TMP_Text>();
        _name.text = GetName();
        _description.text = GetDescription();
    }

    public void TryCast()
    {
        if (CanCast())
        {
            Cast();
        }
    }
}