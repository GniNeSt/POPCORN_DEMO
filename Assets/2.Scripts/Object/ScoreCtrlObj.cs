using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreCtrlObj : MonoBehaviour
{
    TextMeshProUGUI _scoreTMP;
    public void setText(string txt)
    {
        if(_scoreTMP == null) _scoreTMP = GetComponent<TextMeshProUGUI>();
        _scoreTMP.text = txt;
    }
}
