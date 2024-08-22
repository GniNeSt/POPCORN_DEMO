using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseClockWork : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Debuff, "태엽장치(역방향)", "최대 제한 시간이\n5초 줄어든다\n(5보다 낮아지지 않는다.)", "Gear2");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        //
        InGameManager._instance.PlusMaxTime(-5f);
    }
}
