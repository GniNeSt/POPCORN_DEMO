using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrictSupervision : ItemCardCtrlObj
{
    public override void Init()
    {
        base.Init();
        SetCardInfo(CardType.Debuff, "������ �ɻ�", "�÷��̾�� ���� ���� ��\n��ȸ ���� + 1.", "StrictDia");
    }
    public override void CardEffect()
    {
        base.CardEffect();
        //
        InGameManager._instance.SetErrorRisk(1);
    }
}
