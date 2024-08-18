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
    public Action e_OnOverflow;
    public Action e_OnEmpty;
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

    /// <summary>
    /// Receive offensive mana from the other player. Includes mana interaction logic.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    public void ReceiveMana(ManaType type, int count)
    {
        switch (type)
        {
            case ManaType.Red:
                if (White >= count)
                {
                    ChangeMana(ManaType.White, -count);
                }
                else
                {
                    ChangeMana(ManaType.Red, count - White);
                    ChangeMana(ManaType.White, -White);
                }
                break;
            case ManaType.White:
                if (Blue >= count)
                {
                    ChangeMana(ManaType.Blue, -count);
                }
                else
                {
                    ChangeMana(ManaType.White, count - Blue);
                    ChangeMana(ManaType.Blue, -Blue);
                }
                break;
            default:
                ChangeMana(type, count);
                break;
        }
    }

    /// <summary>
    /// Change mana count. Does not include mana interaction logic.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
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
    }

    public bool CheckAlive()
    {
        if (GetFunctionalMana() > MaxMana)
        {
            e_OnOverflow?.Invoke();
            return false;
        }
        else if (GetFunctionalMana() <= 0)
        {
            e_OnEmpty?.Invoke();
            return false;
        }
        return true;
    }

    public int GetFunctionalMana()
    {
        return Blue + Red;
    }

    public void AdvanceRedMana()
    {
        int red = Red;
        ChangeMana(ManaType.Red, -red);
        ChangeMana(ManaType.Black, red);
    }

    public void AdvanceWhiteMana()
    {
        ChangeMana(ManaType.White, -White);
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