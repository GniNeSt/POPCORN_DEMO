using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockWorkObj : ItemCardCtrlObj
{
    protected override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Buff, "보조 태엽 장치");
    }

}
