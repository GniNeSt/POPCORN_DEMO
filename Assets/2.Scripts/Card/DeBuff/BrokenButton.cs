using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenButton : ItemCardCtrlObj
{    
    public override void Init()
    {
        base.Init();

        SetCardInfo(CardType.Debuff, "��ư ������", "�Ƿε� ����ġ�� 1 �����Ѵ�.", "XBtn");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        //
        InGameManager._instance.SetFatigue(1);
    }
}
