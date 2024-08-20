using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaRenderer : MonoBehaviour
{
    public const int MAX_MANA = 12;    
    [SerializeField] private Mana _manaPrefab;
    private Mana[] _manas;
    private RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Render(ManaManager manaManager)
    {
        if (_manas == null)
        {
            _manas = new Mana[MAX_MANA];
            for (int i = 0; i < _manas.Length; i++)
            {
                Mana mana = Instantiate(_manaPrefab, transform);
                _manas[i] = mana;
                mana.RectTransform.anchorMin = new Vector2(0, 0);
                mana.RectTransform.anchorMax = new Vector2(0, 0);
                mana.RectTransform.pivot = new Vector2(0, 0);
                mana.RectTransform.anchoredPosition = new Vector2(i * mana.RectTransform.sizeDelta.x * 1.1f, 0);
                mana.IsPlayerOwned = manaManager.IsPlayer;
            }
        }

        foreach (Mana mana in _manas)
        {
            mana.gameObject.SetActive(true);
        }
        for (int i = manaManager.MaxMana; i < MAX_MANA; i++)
        {
            _manas[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < manaManager.MaxMana; i++)
        {
            _manas[i].Type = ManaType.None;
        }
        int index = 0;
        for (int i = 0; i < manaManager.Blue; i++)
        {
            _manas[index].Type = ManaType.Blue;
            index++;
        }
        for (int i = 0; i < manaManager.Red; i++)
        {
            _manas[index].Type = ManaType.Red;
            index++;
        }
        for (int i = 0; i < manaManager.White; i++)
        {
            _manas[index].Type = ManaType.White;
            index++;
        }
        for (int i = 1; i <= manaManager.Black; i++)
        {
            _manas[manaManager.MaxMana - i].Type = ManaType.Black;
        }

        for (int i = 0; i < _manas.Length; i++)
        {
            _manas[i].Render();
        }

        CenterMana(manaManager);
    }

    private void CenterMana(ManaManager manaManager)
    {
        float totalWidth = 0;
        for (int i = 0; i < manaManager.MaxMana; i++)
        {
            totalWidth += _manas[i].RectTransform.sizeDelta.x * 1.1f;
        }
        _rectTransform.anchoredPosition = new Vector2((_rectTransform.sizeDelta.x - totalWidth) / 2, _rectTransform.anchoredPosition.y);
    }
}