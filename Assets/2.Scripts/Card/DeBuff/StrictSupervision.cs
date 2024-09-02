using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrictSupervision : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Debuff, "엄격한 심사", "플레이어는 조합 실패 시\n기회 차감 + 1.", "StrictDia");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        //
        InGameManager._instance.SetErrorRisk(1);
    }
}
