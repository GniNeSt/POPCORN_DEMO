using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfairJudgment : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Debuff, "�Ұ��� �ǰ�", "���� �Ͽ� ������ �����Ѵ�.", "UnfairJudgment");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        //
        InGameManager._instance.SetSubmitBool(true);
    }
}
