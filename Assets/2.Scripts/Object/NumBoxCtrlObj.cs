using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NumBoxCtrlObj : MonoBehaviour
{
    Animator _animator;
    TextMeshProUGUI[] TMPS;
    NumState _curState;
    bool isChanging;
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
    }
    public void NumChangeDown()
    {
        if (isChanging) return;
        isChanging = true;
        switch (_curState)
        {
            case NumState.zero:
                TMPS[1].text = "1";
                TMPS[2].text = "1";
                _animator.SetInteger("State", 1);
                _curState = NumState.one;
                break;
            case NumState.one:
                TMPS[1].text = "0";
                TMPS[2].text = "0";
                _animator.SetInteger("State", 1);
                _curState = NumState.zero;
                break;
        }
    }
    public void NumChangeReset()
    {
        isChanging = false;
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
    }
}
