using System;
using System.Collections.Generic;
using UnityEngine;
public class ItemManager : MonoBehaviour
{
    ItemCtrlObj[] _ico;

    public enum BuffItem
    {
        //buff
        ClockWorkObj,   //보조 태엽 장치
        FixedSupport,   //고정 지지대
        AuxiliaryCharger,   //보조 충전기


        Max
    }
    public enum DebuffItem
    {
        //debuff
        ReverseClockWork, //태엽 장치(역방향)
        StrictSupervision, //엄격한 심사
        BrokenButton, //고장난 버튼



        Max
    }

    private void Awake()
    {
        _ico = GetComponentsInChildren<ItemCtrlObj>();
    }
    public void ItemSelect()
    {
        foreach (var i in _ico)
        {
            i.RemoveCard();
            i.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false);
        }
    }
    public void SetItem(bool isBuff = true)
    {
        int[] selectNums = new int[3];
        List<int> capacityNums = new List<int>();

        int capacity = 0;
        if (isBuff)
            capacity = (int)BuffItem.Max;
        else
            capacity = (int)DebuffItem.Max;
        for (int num = 0; num < capacity; num++)
        {
            capacityNums.Add(num);
        }

        for (int i = 0; i < selectNums.Length; i++)
        {
            int select = UnityEngine.Random.Range(0, capacityNums.Count - 1);
            selectNums[i] = capacityNums[select];
            capacityNums.Remove(capacityNums[select]);
        }

        int order = 0;
        foreach (var i in _ico)
        {
            ItemCardCtrlObj icco = i.transform.GetChild(0).gameObject.GetComponent<ItemCardCtrlObj>();
            if (icco != null)
            {
                Destroy(icco);
            }
        }
        foreach (var i in _ico)
        {
            Type itemName = null;
            if (isBuff)
                itemName = Type.GetType("" + (BuffItem)selectNums[order++]);
            else
                itemName = Type.GetType("" + (DebuffItem)selectNums[order++]);


            i.SetItem(itemName);
        }

    }
}
