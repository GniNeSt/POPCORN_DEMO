using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSupport : ItemCardCtrlObj
{
    private void Awake()
    {
        Spawn();
    }
    public void Spawn()
    {
        Init();
        SetCardInfo(CardType.Buff, "���� ������", "��� Ȯ�� �е尡\n�������� �ʴ´�.");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        GameObject.FindGameObjectWithTag("HintBox").GetComponent<HintChecker>()._isFixed = true;
    }
}
