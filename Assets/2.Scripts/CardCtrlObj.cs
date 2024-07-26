using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardCtrlObj : MonoBehaviour
{
    TextMeshProUGUI _numTMP;
    private void Awake()
    {
        InitSet("12");
    }
    public void InitSet(string numText)
    {
        _numTMP = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        _numTMP.text = numText;
        _numTMP.enabled = false;
    }
    public void CardFlipToFront()
    {
        _numTMP.enabled = true;
    }
}
