using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemCtrlObj : MonoBehaviour
{
    //[SerializeField] GameObject ItemCard;
    //public void SpawnCard()
    //{
    //    GameObject go = Instantiate(ItemCard);
    //    go.transform.SetParent(transform);
    //    go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    //}
    public void OnClick()
    {
        transform.GetChild(0).GetComponent<ItemCardCtrlObj>().CardEffect();

        transform.parent.GetComponent<ItemManager>().ItemSelect();
    }
    public void RemoveCard()
    {
        GetComponent<Animator>().Play("ItemCardRemove");
    }
    public void AppearCard()
    {
        GetComponent<Animator>().Play("ItemCardSpawn");

    }
}
