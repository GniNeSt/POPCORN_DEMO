using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemManager : MonoBehaviour
{
    ItemCtrlObj[] _ico;
    [SerializeField] GameObject _coverPanel;
    IEnumerator DebuffSelect()
    {
        _coverPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        int count = UnityEngine.Random.Range(20, 50);
        for (int i = 0; i < count; i++)
        {
            _ico[i%3].OnMouseEnter();
            _ico[(i+1) % 3].OnMouseExit();
            _ico[(i+2) % 3].OnMouseExit();

            if (i < count-5)
                yield return new WaitForSeconds(0.07f);
            else
                yield return new WaitForSeconds(0.5f);
        }
        _ico[count % 3].OnMouseEnter();
        _ico[(count + 1) % 3].OnMouseExit();
        _ico[(count + 2) % 3].OnMouseExit();
        yield return new WaitForSeconds(1.3f);
        _ico[count%3].OnClick();
        _coverPanel.SetActive(false);
        yield return null;
    }
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
        _coverPanel.SetActive(false);
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
        if (!isBuff)
            StartCoroutine(DebuffSelect());
    }
}
