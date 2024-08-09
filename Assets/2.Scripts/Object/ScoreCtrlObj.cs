using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreCtrlObj : MonoBehaviour
{
    TextMeshProUGUI _scoreTMP;
    private void Awake()
    {
        _scoreTMP = GetComponent<TextMeshProUGUI>();
    }
    public void setText(string txt)
    {
        _scoreTMP.text = txt;
    }
}
