using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiliaryCharger : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Buff, "���� ������", "��ȸ�� 1 ȸ���Ѵ�.\n(�ִ�ġ�� �ʰ����� �ʴ´�)");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        //
    }
}