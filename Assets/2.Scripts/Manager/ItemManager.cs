using System;
using System.Collections.Generic;
using UnityEngine;
public class ItemManager : MonoBehaviour
{
    ItemCtrlObj[] _ico;

    public enum BuffItem
    {
        //buff
        ClockWorkObj,   //���� �¿� ��ġ
        FixedSupport,   //���� ������
        AuxiliaryCharger,   //���� ������


        Max
    }
    public enum DebuffItem
    {
        //debuff
        ReverseClockWork, //�¿� ��ġ(������)
        StrictSupervision, //������ �ɻ�
        BrokenButton, //���峭 ��ư



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
