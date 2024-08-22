using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrust : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Buff, "������ �ŷ�", "��ư ����ġ��\n1 �����Ѵ�.\n(1�̸����� �������� �ʴ´�.)", "ActiveTrust");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        //
        InGameManager._instance._btnRisk--;
        if (InGameManager._instance._btnRisk < 1)
        {
            InGameManager._instance._btnRisk = 1;
        }
    }
}

