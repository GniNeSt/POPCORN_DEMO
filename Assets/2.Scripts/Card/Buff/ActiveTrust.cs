using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrust : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Buff, "적극적 신뢰", "버튼 증가치가\n1 감소한다.\n(1미만으로 떨어지지 않는다.)", "ActiveTrust");
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

