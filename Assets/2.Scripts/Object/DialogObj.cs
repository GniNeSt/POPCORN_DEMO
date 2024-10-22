using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogObj : MonoBehaviour//PopUpUI Load로 Init함수를 호출하자!!!
{
    //참조
    [SerializeField] TMP_Text _dialogText;
    [SerializeField] TMP_Text[] _fadeoutTxt;
    [SerializeField] float _nextTextDelayTime = 0.1f;
    Canvas _canvas;
    [SerializeField] Image[] _bgImg;
    //변수
    bool _omission;//생략 Bool
    bool _newOrder;
    float _curtime;
    IEnumerator SequentialPrint(string text, bool wait = false)
    {
        while (_newOrder)
        {
            if(!wait)
                _omission = true;
            yield return new WaitForSeconds(_nextTextDelayTime);
        }
        _omission = false;
        transform.GetChild(0).gameObject.SetActive(true);
        _dialogText.color = Color.white;
        foreach (var tmp in _fadeoutTxt)
        {
            tmp.color = Color.white;
        }
        foreach (var img in _bgImg)
        {
            img.color = Color.white;
        }
        _newOrder = true;
        //초기화
        _omission = false;
        _dialogText.text = "_";
        string temp = "";
        //순차 출력
        foreach (var t in text)
        {
            _dialogText.text = temp + "_";    //타자 연출
            temp += t;  //저장
            SoundManager._instance.PlaySFX(SoundManager.SFXClipName.OldTypewriter);
            yield return new WaitForSeconds(_nextTextDelayTime);  //출력 딜레이
           
            if (_omission)
            {
                _omission = false;

                break;
            }
        }
        _dialogText.text = text;

        while (wait)
        {
            if (_omission)
            {
                _omission = false;
                InGameManager._instance._dialogClickEvent = true;
                break;
            }
            yield return new WaitForSeconds(_nextTextDelayTime);
        }
        if(!wait) yield return new WaitForSeconds(_nextTextDelayTime);
        _newOrder = false;
        yield return null;
    }
    private void Update()
    {
        if (!_newOrder)
        {
            _curtime += Time.deltaTime;
            if(_curtime >= 2.0f)
            {
                Color color = _dialogText.color;
                if (color.a > 0.001f)
                {
                    color.a -= Time.deltaTime * 2f;
                    _dialogText.color = color;
                    foreach (var tmp in _fadeoutTxt)
                    {
                        tmp.color = color;
                    }
                    foreach (var img in _bgImg)
                    {
                        img.color = color;
                    }
                    if (_omission)
                    {
                        _omission = false;
                        _curtime = 2.5f;
                    }
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
    public void PrintTxt(string text, bool wait = false)
    {
        _curtime = 0;
        //순차 출력 Coroutine?
        StartCoroutine(SequentialPrint(text, wait));
    }
    public void InitDalog()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _dialogText.text = "오류 :\n텍스트가 초기화 되지 않았습니다.";
    }
    public void SwitchOmission()
    {
        _omission = true;
    }

    //private void OnGUI()
    //{
    //    GUIStyle style = new GUIStyle("button");
    //    style.fontSize = 36;

    //    if (GUI.Button(new Rect(120, 0, 120, 40), "Print", style))
    //    {
    //        //PrintTxt("안녕하세요! 새로운 출력\n기능입니다!!!");
    //        if (transform.GetChild(0).gameObject.activeSelf)
    //        {
    //            transform.GetChild(0).gameObject.SetActive(false);
    //            return;
    //        }
    //        else
    //        {
    //            transform.GetChild(0).gameObject.SetActive(true);
    //            PrintTxt("Nice to meet you.\nWelcome to the popcorn game.");
    //        }
    //    }
    //}
}
