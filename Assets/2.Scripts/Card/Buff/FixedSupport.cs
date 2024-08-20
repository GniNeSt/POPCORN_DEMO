using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSupport : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Buff, "고정 지지대", "결과 확인 패드가\n가려지지 않는다.");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        GameObject.FindGameObjectWithTag("HintBox").GetComponent<HintChecker>()._isFixed = true;
    }
}
