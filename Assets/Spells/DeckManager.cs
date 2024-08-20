using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DeckRenderer))]
public class DeckManager : Singleton<DeckManager>
{
    public List<Spell> Deck { get; private set; }
    public List<Spell> DrawPile { get; private set; }
    public List<Spell> Hand { get; private set; }
    public List<Spell> DiscardPile { get; private set; }
    public List<Spell> ExhaustPile { get; private set; }
    public List<Spell> Database { get; private set; }
    public List<Spell> Shop { get; private set; }
    [SerializeField] private RectTransform _cardPrefab;
    private DeckRenderer _deckRenderer;

    protected override void Awake()
    {
        base.Awake();
        _deckRenderer = GetComponent<DeckRenderer>();
    }

    public override void Initialize()
    {
        InitializeDeck();
        DrawPile = new List<Spell>();
        Hand = new List<Spell>();
        ExhaustPile = new List<Spell>();
        DiscardPile = new List<Spell>();
        Shop = new List<Spell>();
        InitializeDrawPile();
        DrawNewHand();
        InitializeDatabase();
    }

    // Instantiate Spell game objects and add them to the Draw pile, shuffled.
    public void InitializeDeck()
    {
        Deck = new List<Spell>();
        // AddCardToDeck<Blue>(3);
        // AddCardToDeck<Red>(1);
        // AddCardToDeck<White>(2);
        // AddCardToDeck<SpellProjection>(1);
        AddCardToDeck<SpellBasicOffensive>(2);
        // AddCardToDeck<SpellFire>(1);
        // AddCardToDeck<SpellSteal>(1);

        AddCardToDeck<Blue>(5);
        AddCardToDeck<SpellLightShield>(3);
    }

    private void InitializeDatabase()
    {
        Database = new List<Spell>();
        AddCardToDatabase<SpellFire>(2);
        AddCardToDatabase<SpellRedReversal>(2);
        AddCardToDatabase<SpellWhiteReversal>(2);
        AddCardToDatabase<SpellRemove>(2);
        AddCardToDatabase<SpellSteal>(2);
        AddCardToDatabase<SpellLightShield>(2);
        AddCardToDatabase<SpellFocus>(2);
        AddCardToDatabase<SpellDrain>(2);
        AddCardToDatabase<SpellMemory>(2);
        AddCardToDatabase<SpellCast>(2);
        AddCardToDatabase<SpellDestroy>(2);
        AddCardToDatabase<Red>(2);
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

    public void Reset()
    {
        foreach (Spell spell in Hand)
        {
            DiscardPile.Add(spell);
        }
        Hand.Clear();
        foreach (Spell spell in DiscardPile)
        {
            DrawPile.Add(spell);
        }
        DiscardPile.Clear();
        foreach (Spell spell in ExhaustPile)
        {
            DrawPile.Add(spell);
        }
        ExhaustPile.Clear();
        Shuffle(DrawPile);
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

    private void AddCardToDatabase<T>(int count) where T : Spell
    {
        for (int i = 0; i < count; i++)
        {
            RectTransform card = Instantiate(_cardPrefab);
            T spell = card.AddComponent<T>();
            Database.Add(spell);
        }
    }

    public void AddCardToDeck(Spell spell)
    {
        RectTransform card = Instantiate(_cardPrefab);
        spell = card.gameObject.AddComponent(spell.GetType()) as Spell;
        Deck.Add(spell);
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

    public Spell GetRandomDatabaseSpell()
    {
        return Database[Random.Range(0, Database.Count)];
    }

    public void DatabaseToShop(Spell spell)
    {
        MoveCard(spell, Database, Shop);
    }

    public void ShopToDatabase(Spell spell)
    {
        MoveCard(spell, Shop, Database);
    }

    public void ShopToDeck(Spell spell)
    {
        MoveCard(spell, Shop, DiscardPile);
        spell.gameObject.SetActive(false);
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