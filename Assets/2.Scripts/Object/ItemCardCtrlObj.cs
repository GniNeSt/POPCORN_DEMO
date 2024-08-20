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
    public virtual void Init()
    {
        _BGImg = transform.GetChild(0).GetComponent<Image>();
        _IconImg = _BGImg.transform.GetChild(0).GetComponent<Image>();
        _nameTMP = _BGImg.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _effectTMP = _BGImg.transform.GetChild(2).GetComponent<TextMeshProUGUI>();


    }
    public virtual void CardEffect()
    {
        Debug.Log(this + "카드 효과 사용!");
        DialogManager _dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();
        if (_dialogManager == null)
        {
            Debug.Log("dialogManager가 초기화 되지 않았습니다.");
        }
        else if(_cardType == CardType.Buff)
        {
            _dialogManager.PrintDialog(0, DialogManager.DialogProperty.Item);
        }
        else
        {
            _dialogManager.PrintDialog(1, DialogManager.DialogProperty.Item);

        }
        
        InGameManager._instance.SetGameStatus(InGameManager.InGameStatus.InGame);
        InGameManager._instance.DealerTurnStart();
    }

}
