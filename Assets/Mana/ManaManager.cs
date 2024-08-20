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
    private bool _checkAliveQueued;

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

    public void Initialize()
    {
        _manaRenderer = GetComponent<ManaRenderer>();
        _manaRenderer.Render(this);
    }

    void Update()
    {
        if (_checkAliveQueued)
        {
            CheckAlive();
            _checkAliveQueued = false;
        }
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
        QueueCheckAlive();
    }

    public void ResetMana()
    {
        Blue = MaxMana / 2;
        Red = 0;
        White = 0;
        Black = 0;
        _manaRenderer.Render(this);
    }

    public void QueueCheckAlive()
    {
        _checkAliveQueued = true;
    }

    public bool CheckAlive()
    {
        Debug.Log($"{gameObject.name} {GetFunctionalMana()} / {GetAvailableMana()}");
        if (GetFunctionalMana() > GetAvailableMana())
        {
            GameManager.Instance.SetDefeatDescription($"You died because you had too much mana ({GetFunctionalMana()}/{GetAvailableMana()})!");
            e_OnOverflow?.Invoke();
            return false;
        }
        else if (GetFunctionalMana() <= 0)
        {
            GameManager.Instance.SetDefeatDescription($"You died because you ran out of mana ({GetFunctionalMana()}/{GetAvailableMana()})!");
            e_OnEmpty?.Invoke();
            return false;
        }
        return true;
    }

    public int GetFunctionalMana()
    {
        return Blue + Red + White;
    }

    public int GetAvailableMana()
    {
        return MaxMana - Black;
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