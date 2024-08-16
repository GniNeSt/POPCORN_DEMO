using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardCtrlObj : MonoBehaviour
{
    CardType _cardType;
    string _cardName;
    public enum CardType
    {
        Buff,
        Debuff
    }
    protected void SetCardInfo(CardType ct, string str)
    {
        _cardType = ct;
        _cardName = str;
    }
    protected virtual void Init()
    {

    }
    protected virtual void CardEffect()
    {
        Debug.Log(name + "카드 효과 사용!");
    }
}
