using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaRenderer : MonoBehaviour
{
    [SerializeField] private Mana _manaPrefab;
    private Mana[] _manas;

    public void RenderMana(ManaManager manaManager)
    {
        if (_manas == null)
        {
            _manas = new Mana[manaManager.MaxMana];
            for (int i = 0; i < _manas.Length; i++)
            {
                Mana mana = Instantiate(_manaPrefab, transform);
                _manas[i] = mana;
                mana.RectTransform.anchorMin = new Vector2(0, 0);
                mana.RectTransform.anchorMax = new Vector2(0, 0);
                mana.RectTransform.pivot = new Vector2(0, 0);
                mana.RectTransform.anchoredPosition = new Vector2(i * mana.RectTransform.sizeDelta.x * 1.1f, 0);
            }
        }

        if (manaManager.Blue + manaManager.Red + manaManager.White + manaManager.Black > _manas.Length)
        {
            Debug.LogError("Not enough mana slots to render mana");
            return;
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
        for (int i = 0; i < manaManager.Black; i++)
        {
            _manas[index].Type = ManaType.Black;
            index++;
        }
        for (int i = index; i < _manas.Length; i++)
        {
            _manas[i].Type = ManaType.None;
        }

        for (int i = 0; i < _manas.Length; i++)
        {
            _manas[i].Render();
        }
    }
}
