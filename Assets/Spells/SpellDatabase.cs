using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SpellDatabase : Singleton<SpellDatabase>
{
    public List<Spell> AllSpells;
    [SerializeField] private RectTransform _cardPrefab;

    public override void Initialize()
    {
        LoadSpell<SpellFire>();
        LoadSpell<SpellRedReversal>();
        LoadSpell<SpellWhiteReversal>();
        LoadSpell<SpellRemove>();
        LoadSpell<SpellSteal>();
    }

    private void LoadSpell<T>() where T : Spell
    {
        RectTransform card = Instantiate(_cardPrefab);
        T spell = card.AddComponent<T>();
        AllSpells.Add(spell);
    }

    public Spell GetRandomSpell()
    {
        Spell spell = AllSpells[Random.Range(0, AllSpells.Count)];
        Spell copy = Instantiate(spell);
        return copy;
    }
}