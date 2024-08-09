using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HintChecker : MonoBehaviour
{
    Image _mainImg, _curvImg;
    TextMeshProUGUI _resultTMP;
    RectTransform _curvRectTransform;


    Vector2 _curvPos;
    float _curvHeight;
    [SerializeField] float _downSpeed = 2.0f;
    CheckerStatus _curCheckStat;
    public enum CheckerStatus
    {
        Down,
        Up
    }
    IEnumerator CurvDown()
    {
        while(_curvRectTransform.anchoredPosition.y >= _curvPos.y && _curCheckStat == CheckerStatus.Up)
        {
            _curvRectTransform.anchoredPosition -= Vector2.up * _downSpeed;
            yield return new WaitForSeconds(0.1f);
        }
        _curCheckStat = CheckerStatus.Down;
       yield return null;
    }
    private void Awake()
    {
        _mainImg = transform.GetChild(0).GetComponent<Image>();
        _curvImg = transform.GetChild(1).GetComponent<Image>();
        _resultTMP = _mainImg.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _curvRectTransform = _curvImg.GetComponent<RectTransform>();
        _curvHeight = _curvRectTransform.sizeDelta.y;
        _curvPos = _curvRectTransform.anchoredPosition;
    }
    public void SetResult(string result)
    {
        _resultTMP.text = result;
    }
    public void PointDwonEvent()
    {
        _curvRectTransform.anchoredPosition = _curvPos + Vector2.up * _curvHeight * 0.8f;
        //_resultTMP.text = ""+InGameManager._instance._curResult;
        _curCheckStat = CheckerStatus.Down;
    }
    public void PointUpEvent()
    {
        _curCheckStat = CheckerStatus.Up;
        StartCoroutine(CurvDown());
    }
}
