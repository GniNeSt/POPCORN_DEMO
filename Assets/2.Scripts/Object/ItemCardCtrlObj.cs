using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemCardCtrlObj : MonoBehaviour
{
    public Sprite _BGSprite,_IconSprite;
    public string _cardName;
    [TextArea]public string _effectString;
    public CardType _cardType;
    Image _BGImg, _IconImg;
    TextMeshProUGUI _nameTMP, _effectTMP;
    DialogManager _dialogManager;
    
    public enum CardType
    {
        Buff,
        Debuff
    }
    protected void SetCardInfo(CardType ct, string name, string effect) // icon sprite 추가
    {
        _cardType = ct;
        _cardName = name;
        _effectString = effect;

        _nameTMP.text = name;
        _effectTMP.text = effect;
    }
    protected virtual void Init()
    {
        _BGImg = transform.GetChild(0).GetComponent<Image>();
        _IconImg = _BGImg.transform.GetChild(0).GetComponent<Image>();
        _nameTMP = _BGImg.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _effectTMP = _BGImg.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        _dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();
    }
    public virtual void CardEffect()
    {
        Debug.Log(name + "카드 효과 사용!");
        if(_cardType == CardType.Buff)
        {
            _dialogManager.PrintDialog(0, DialogManager.DialogProperty.Item);
        }
        else
        {
            _dialogManager.PrintDialog(1, DialogManager.DialogProperty.Item);

        }
        //ingameManageer에서 카드 제거
    }

}
