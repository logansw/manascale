using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    public bool IsSelected;
    public bool IsPlayerOwned;
    public ManaType Type;
    [SerializeField] private Image _fill;
    [SerializeField] private Image _stroke;
    public RectTransform RectTransform;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TrySelect);
    }

    public void Render()
    {
        RectTransform.anchorMin = new Vector2(0, 0);
        RectTransform.anchorMax = new Vector2(0, 0);
        _fill.color = Type switch
        {
            ManaType.Blue => Color.blue,
            ManaType.Red => Color.red,
            ManaType.White => Color.white,
            ManaType.Black => Color.black,
            _ => new Color(1f, 1f, 1f, 0.25f)
        };
        _stroke.gameObject.SetActive(IsSelected);
    }

    public void TrySelect()
    {
        if (SpellManager.Instance.SpellSelectState != SpellSelectState.ChoosingTarget || Type == ManaType.None || Type == ManaType.Black)
        {
            return;
        }

        if (IsPlayerOwned)
        {
            if (IsSelected)
            {
                IsSelected = false;
                SpellManager.Instance.SelectedManaPlayer.Remove(this);
            }
            else
            {
                IsSelected = true;
                SpellManager.Instance.SelectedManaPlayer.Add(this);
            }
        }
        else
        {
            if (IsSelected)
            {
                IsSelected = false;
                SpellManager.Instance.SelectedManaEnemy.Remove(this);
            }
            else
            {
                IsSelected = true;
                SpellManager.Instance.SelectedManaEnemy.Add(this);
            }
        }
        Render();
        SpellManager.Instance.UpdateCastButton();
    }
}