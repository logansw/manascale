using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckRenderer : MonoBehaviour
{
    [SerializeField] private RectTransform _cardPrefab;
    [SerializeField] private RectTransform _drawPileTransform;
    [SerializeField] private RectTransform _handTransform;
    [SerializeField] private RectTransform _discardPileTransform;
    [SerializeField] private RectTransform _exhaustPileTransform;
    private bool _updateQueued;

    void Update()
    {
        if (_updateQueued)
        {
            _updateQueued = false;
            RenderHand(DeckManager.Instance.Hand);
            RenderDrawPile(DeckManager.Instance.DrawPile);
            RenderDiscardPile(DeckManager.Instance.DiscardPile);
        }
    }

    public void RenderHand(List<Spell> hand)
    {
        float cardPositionOffset = 0;
        foreach (Spell spell in hand)
        {
            spell.gameObject.SetActive(true);
            spell.transform.SetParent(_handTransform);
            spell.RectTransform.anchorMin = new Vector2(0, 0.5f);
            spell.RectTransform.anchorMax = new Vector2(0, 0.5f);
            spell.RectTransform.pivot = new Vector2(0, 0.5f);
            spell.RectTransform.anchoredPosition = new Vector2(cardPositionOffset, 0);
            cardPositionOffset += _cardPrefab.rect.width + 10;
        }
    }

    public void RenderDrawPile(List<Spell> drawPile)
    {
        foreach (Spell spell in drawPile)
        {
            spell.transform.SetParent(_drawPileTransform);
            spell.gameObject.SetActive(false);
            spell.transform.localPosition = Vector3.zero;
        }
    }

    public void RenderDiscardPile(List<Spell> discardPile)
    {
        foreach (Spell spell in discardPile)
        {
            spell.transform.SetParent(_discardPileTransform);
            spell.gameObject.SetActive(false);
            spell.transform.localPosition = Vector3.zero;
        }
    }

    public void QueueUpdate()
    {
        _updateQueued = true;
    }
}