using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    ItemCtrlObj[] _ico;

    public enum ItemKind
    {

    }

    private void Awake()
    {
        _ico = GetComponentsInChildren<ItemCtrlObj>();
    }
    public void ItemSelect()
    {
        foreach(var i in _ico)
        {
            i.RemoveCard();
            i.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false);
        }
    }
    public void SetItem()
    {

    }
}
