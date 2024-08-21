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
    [SerializeField] ItemCardCtrlObj _icco;
    public void OnClick()
    {
        _icco.CardEffect();

        transform.parent.GetComponent<ItemManager>().ItemSelect();
    }
    public void SetItem(System.Type type)
    {
        transform.GetChild(0).gameObject.AddComponent(type);
        transform.GetChild(0).GetComponent<ItemCardCtrlObj>().Init();
        transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
        AppearCard();
    }
    public void SetEffect(ItemCardCtrlObj obj)
    {
        _icco = obj;
    }
    public void RemoveCard()
    {
        Destroy(_icco);
        GetComponent<Animator>().Play("ItemCardRemove");
    }
    public void AppearCard()
    {
        GetComponent<Animator>().Play("ItemCardSpawn");
    }
}
