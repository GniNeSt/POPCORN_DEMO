using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseClockWork : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Debuff, "�¿���ġ(������)", "�ִ� ���� �ð���\n5�� �پ���\n(5���� �������� �ʴ´�.)", "Gear2");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        //
        InGameManager._instance.PlusMaxTime(-5f);
    }
}
