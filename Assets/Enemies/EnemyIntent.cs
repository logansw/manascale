using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIntent : MonoBehaviour
{
    // [SerializeField] private RectTransform _manaIcon;
    // [SerializeField] private RectTransform _costFrame;
    // [SerializeField] private RectTransform _sendFrame;
    // [SerializeField] private RectTransform _generateFrame;
    [SerializeField] private TMP_Text _description;

    public void RenderIntent(EnemySpellSO spell)
    {
        _description.text = spell.GetDescription();
        // RenderCost(spell);
        // RenderSend(spell);
        // RenderGenerate(spell);
    }

    /*
    private void RenderCost(EnemySpellSO spell)
    {
        int index = 0;
        for (int i = 0; i < spell.BlueCost; i++)
        {
            RenderMana(index, Color.blue, _costFrame);
            index++;
        }
        for (int i = 0; i < spell.RedCost; i++)
        {
            RenderMana(index, Color.red, _costFrame);
            index++;
        }
    }

    private void RenderSend(EnemySpellSO spell)
    {
        int index = 0;
        for (int i = 0; i < spell.BlueSend; i++)
        {
            RenderMana(index, Color.blue, _sendFrame);
            index++;
        }
        for (int i = 0; i < spell.RedSend; i++)
        {
            RenderMana(index, Color.red, _sendFrame);
            index++;
        }
        for (int i = 0; i < spell.WhiteSend; i++)
        {
            RenderMana(index, Color.white, _sendFrame);
            index++;
        }
        for (int i = 0; i < spell.BlackSend; i++)
        {
            RenderMana(index, Color.black, _sendFrame);
            index++;
        }
    }

    private void RenderGenerate(EnemySpellSO spell)
    {
        int index = 0;
        for (int i = 0; i < spell.BlueGenerate; i++)
        {
            RenderMana(index, Color.blue, _generateFrame);
            index++;
        }
        for (int i = 0; i < spell.WhiteGenerate; i++)
        {
            RenderMana(index, Color.white, _generateFrame);
            index++;
        }
    }

    private void RenderMana(int index, Color color, RectTransform frame)
    {
        RectTransform manaIcon = Instantiate(_manaIcon, frame);
        manaIcon.transform.position = new Vector3(index * manaIcon.rect.width, 0, 0);
        manaIcon.GetComponent<Image>().color = color;
    }
    */
}
