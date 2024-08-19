using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockWorkObj : ItemCardCtrlObj
{
    private void Awake()
    {
        Spawn();
    }
    public void Spawn()
    {
        Init();
        SetCardInfo(CardType.Buff, "보조 태엽 장치", "시간 + 5");        
    }
    public override void CardEffect()
    {
        base.CardEffect();
        InGameManager._instance.PlusMaxTime(5f);
    }
}
