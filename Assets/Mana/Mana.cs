using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    public ManaType Type;
    [SerializeField] private Image _image;
    public RectTransform RectTransform;

    public void Render()
    {
        RectTransform.anchorMin = new Vector2(0, 0);
        RectTransform.anchorMax = new Vector2(0, 0);
        _image.color = Type switch
        {
            ManaType.Blue => Color.blue,
            ManaType.Red => Color.red,
            ManaType.White => Color.white,
            ManaType.Black => Color.black,
            _ => Color.clear
        };
    }
}
