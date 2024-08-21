using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NumBoxCtrlObj : MonoBehaviour
{
    Animator _animator;
    TextMeshProUGUI[] TMPS;
    TextMeshProUGUI _fatigueTmp;
    NumState _curState;
    public bool isChanging { get; set; }
    [SerializeField] int powNum;
    public NumState _state { get { return _curState; } }
    public enum NumState
    {
        zero,
        one
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _curState = NumState.zero;
        Transform tmps = transform.GetChild(0).GetChild(0);
        TMPS = new TextMeshProUGUI[tmps.childCount];
        for (int num = 0; num < tmps.childCount; num++)
        {
            TMPS[num] = tmps.GetChild(num).GetComponent<TextMeshProUGUI>();
        }

        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+Mathf.Pow(2,powNum);
        _fatigueTmp = GameObject.FindGameObjectWithTag("Fatigue").GetComponent<TextMeshProUGUI>();
        _fatigueTmp.text = "0";
    }
    public void NumChangeDown()
    {
        if (isChanging) return;
        isChanging = true;

        InGameManager inGameManager = InGameManager._instance;
        SoundManager._instance.PlaySFX(SoundManager.SFXClipName.Heal);

        switch (_curState)
        {
            case NumState.zero:
                TMPS[1].text = "1";
                TMPS[2].text = "1";
                _animator.SetInteger("State", 1);
                _curState = NumState.one;
                inGameManager.setBinarySetting(powNum, true);
                break;
            case NumState.one:
                TMPS[1].text = "0";
                TMPS[2].text = "0";
                _animator.SetInteger("State", 1);
                _curState = NumState.zero;
                inGameManager.setBinarySetting(powNum, false);
                break;
        }
        int value = int.Parse(_fatigueTmp.text) + inGameManager._btnRisk;
        _fatigueTmp.text = (value) + "";
        if(value >= 20)
        {
            _fatigueTmp.text = "0";
            inGameManager._isBuffItem = false;
            inGameManager.SetGameStatus(InGameManager.InGameStatus.Item);
        }
        //애니메이션 마지막 프레임에 state 리셋 버그확인, IngameManager
    }
    public void NumChangeReset()
    {
        switch (_curState)
        {
            case NumState.zero:
                TMPS[0].text = "0";
                TMPS[2].text = "1";
                break;
            case NumState.one:
                TMPS[0].text = "1";
                TMPS[2].text = "0";
                break;
        }
        _animator.SetInteger("State", 0);
        isChanging = false;
    }
    public void NumReset()
    {
        switch (_curState)
        {
            case NumState.zero:
                break;
            case NumState.one:
                TMPS[1].text = "0";
                TMPS[2].text = "0";
                _animator.SetInteger("State", 1);
                _curState = NumState.zero;
                break;
        }
        isChanging = false;
    }
}
