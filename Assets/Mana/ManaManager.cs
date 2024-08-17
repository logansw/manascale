using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ManaRenderer))]
public class ManaManager : MonoBehaviour
{
    public bool IsPlayer;
    public static ManaManager InstancePlayer;
    public static ManaManager InstanceEnemy;
    public static Action e_OnOverflow;
    public static Action e_OnEmpty;
    public int MaxMana;
    public int Blue;
    public int Red;
    public int White;
    public int Black;
    private ManaRenderer _manaRenderer;

    void Awake()
    {
        if (IsPlayer)
        {
            InstancePlayer = this;
        }
        else
        {
            InstanceEnemy = this;
        }
    }

    void Start()
    {
        _manaRenderer = GetComponent<ManaRenderer>();
        _manaRenderer.Render(this);
    }

    public int GetMana(ManaType type)
    {
        switch (type)
        {
            case ManaType.Blue:
                return Blue;
            case ManaType.Red:
                return Red;
            case ManaType.White:
                return White;
            case ManaType.Black:
                return Black;
            default:
                return 0;
        }
    }

    public void ChangeMana(ManaType type, int count)
    {
        switch (type)
        {
            case ManaType.Blue:
                Blue += count;
                break;
            case ManaType.Red:
                Red += count;
                break;
            case ManaType.White:
                White += count;
                break;
            case ManaType.Black:
                Black += count;
                break;
        }
        _manaRenderer.Render(this);

        if (GetFunctionalMana() <= 0 || GetFunctionalMana() > MaxMana)
        {
            e_OnOverflow?.Invoke();
        }
        else if (GetFunctionalMana() == 0)
        {
            e_OnEmpty?.Invoke();
        }
    }

    public int GetFunctionalMana()
    {
        return Blue + Red;
    }
}

public enum ManaType
{
    Blue,
    Red,
    White,
    Black,
    None,
}