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
            ManaType.Blue => new Color(51f/255f, 77f/255f, 195f/255f, 1f),
            ManaType.Red => new Color(195f/255f, 50f/255f, 50f/255f, 1f),
            ManaType.White => new Color(223f/255f, 223f/255f, 223f/255f, 1f),
            ManaType.Black => Color.black,
            _ => new Color(1f, 1f, 1f, 0.40f)
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
            SpellManager.Instance.TryTogglePlayerMana(this);
        }
        else
        {
            SpellManager.Instance.TryToggleEnemyMana(this);
        }
        Render();
        SpellManager.Instance.UpdateCastButton();
    }
}