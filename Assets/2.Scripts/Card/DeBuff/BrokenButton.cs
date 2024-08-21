using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenButton : ItemCardCtrlObj
{    
    public override void Init()
    {
        base.Init();

        SetCardInfo(CardType.Debuff, "버튼 과부하", "피로도 증가치가 1 증가한다.");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        //
        InGameManager._instance._btnRisk++;
    }
}
