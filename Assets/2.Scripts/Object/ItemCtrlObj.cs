using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrlObj : MonoBehaviour
{
    public void OnClick()
    {
        transform.GetChild(0).GetComponent<ItemCardCtrlObj>().CardEffect();

        transform.parent.GetComponent<ItemManager>().ItemSelect();
    }
    public void RemoveCard()
    {
        GetComponent<Animator>().SetBool("Click", true);
    }
}
