using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfairJudgment : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Debuff, "불공정 판결", "다음 턴에 무조건 실패한다.", "UnfairJudgment");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        //
        InGameManager._instance.SetSubmitBool(true);
    }
}
