using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockWorkObj : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Buff, "보조 태엽 장치", "시간 + 5");   
    }
    public override void CardEffect()
    {
        base.CardEffect();
        InGameManager._instance.PlusMaxTime(5f);
    }
}
