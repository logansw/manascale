using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ManaRenderer))]
public class ManaManager : MonoBehaviour
{
    public int MaxMana;
    public int Blue;
    public int Red;
    public int White;
    public int Black;
    private ManaRenderer _manaRenderer;

    void Start()
    {
        _manaRenderer = GetComponent<ManaRenderer>();
    }

    void Update()
    {
        _manaRenderer.RenderMana(this);
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