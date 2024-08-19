using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DeckRenderer))]
public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;
    public List<Spell> Deck { get; private set; }
    public List<Spell> DrawPile { get; private set; }
    public List<Spell> Hand { get; private set; }
    public List<Spell> DiscardPile { get; private set; }
    public List<Spell> ExhaustPile { get; private set; }
    [SerializeField] private RectTransform _cardPrefab;
    private DeckRenderer _deckRenderer;

    void Awake()
    {
        Instance = this;
        _deckRenderer = GetComponent<DeckRenderer>();
    }

    void Start()
    {
        InitializeDeck();
            DrawPile = new List<Spell>();
        Hand = new List<Spell>();
        ExhaustPile = new List<Spell>();
        DiscardPile = new List<Spell>();
        InitializeDrawPile();
        DrawNewHand();
    }

    // Instantiate Spell game objects and add them to the Draw pile, shuffled.
    public void InitializeDeck()
    {
        Deck = new List<Spell>();
        AddCardToDeck<Blue>(3);
        AddCardToDeck<Red>(1);
        AddCardToDeck<White>(2);
        AddCardToDeck<SpellProjection>(1);
        AddCardToDeck<SpellBasicOffensive>(2);
        AddCardToDeck<SpellFire>(1);
        AddCardToDeck<SpellSteal>(1);
    }

    public void Shuffle(List<Spell> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Spell value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void InitializeDrawPile()
    {
        foreach (Spell spell in Deck)
        {
            DrawPile.Add(spell);
        }
        Shuffle(DrawPile);
        Deck.Clear();
    }

    public void AddCardToDeck<T>(int count) where T : Spell
    {
        for (int i = 0; i < count; i++)
        {
            RectTransform card = Instantiate(_cardPrefab);
            T spell = card.AddComponent<T>();
            Deck.Add(spell);
        }
    }

    public void DrawNewHand()
    {
        foreach (Spell spell in Hand)
        {
            DiscardPile.Add(spell);
        }
        Hand.Clear();
        DrawCards(5);
    }

    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            DrawCard();
        }
    }

    private void DrawCard()
    {
        if (DrawPile.Count == 0)
        {
            foreach (Spell discardedSpell in DiscardPile)
            {
                DrawPile.Add(discardedSpell);
            }
            DiscardPile.Clear();
            Shuffle(DrawPile);
        }
        Spell spell = DrawPile[0];
        MoveCard(spell, DrawPile, Hand);
    }

    public void Discard(Spell spell)
    {
        if (!Hand.Contains(spell))
        {
            Debug.LogError("The spell you are trying to discard is not in your hand");
            return;
        }

        MoveCard(spell, Hand, DiscardPile);
    }

    public void Exhaust(Spell spell)
    {
        if (!Hand.Contains(spell))
        {
            Debug.LogError("The spell you are trying to discard is not in your hand");
            return;
        }

        MoveCard(spell, Hand, ExhaustPile);
    }

    private void MoveCard(Spell spell, List<Spell> from, List<Spell> to)
    {
        if (!from.Contains(spell))
        {
            Debug.LogError("The spell you are trying to move is not in the source list");
            return;
        }
        to.Add(spell);
        from.Remove(spell);
        _deckRenderer.QueueUpdate();
    }
}