using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpell", menuName = "EnemySpell")]
public class EnemySpellSO : ScriptableObject
{
    [Header("Cost")]
    public int BlueCost;
    public int RedCost;
    
    [Header("Effect")]
    public int BlueSend;
    public int RedSend;
    public int WhiteSend;
    public int BlackSend;
    public int BlueGenerate;
    public int WhiteGenerate;

    public bool CanCast()
    {
        return ManaManager.InstanceEnemy.Blue >= BlueCost && ManaManager.InstanceEnemy.Red >= RedCost;
    }
    
    public string GetDescription()
    {
        StringBuilder sb = new StringBuilder();
        if (BlueCost > 0)
        {
            sb.Append("Spend ");
            sb.Append(BlueCost);
            sb.Append(" Blue");
        }
        if (RedCost > 0)
        {
            if (sb.Length > 0)
            {
                sb.Append(", ");
            }
            sb.Append("Spend ");
            sb.Append(RedCost);
            sb.Append(" Red");
        }
        if (BlueSend > 0)
        {
            sb.Append("Attack with ");
            sb.Append(BlueSend);
            sb.Append(" Blue");
        }
        if (RedSend > 0)
        {
            if (sb.Length > 0)
            {
                sb.Append(", ");
            }
            sb.Append("Attack with ");
            sb.Append(RedSend);
            sb.Append(" Red");
        }
        if (WhiteSend > 0)
        {
            if (sb.Length > 0)
            {
                sb.Append(", ");
            }
            sb.Append("Attack with ");
            sb.Append(WhiteSend);
            sb.Append(" White");
        }
        if (BlackSend > 0)
        {
            if (sb.Length > 0)
            {
                sb.Append(", ");
            }
            sb.Append("Attack with ");
            sb.Append(BlackSend);
            sb.Append(" Black");
        }
        if (BlueGenerate > 0)
        {
            if (sb.Length > 0)
            {
                sb.Append(", ");
            }
            sb.Append("Generate ");
            sb.Append(BlueGenerate);
            sb.Append(" Blue");
        }
        if (WhiteGenerate > 0)
        {
            if (sb.Length > 0)
            {
                sb.Append(", ");
            }
            sb.Append("Generate ");
            sb.Append(WhiteGenerate);
            sb.Append(" White");
        }
        return sb.ToString();
    }
}