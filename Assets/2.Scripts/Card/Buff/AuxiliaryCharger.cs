using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiliaryCharger : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Buff, "보조 충전기", "기회를 1 회복한다.\n(최대치를 초과하지 않는다)", "Charger");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        //
        InGameManager._instance.RecoverHP(1);
    }
}
