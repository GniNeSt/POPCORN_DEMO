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

    IEnumerator SequentialPrint(string text)
    {
        //초기화
        _omission = false;
        _dialogText.text = "_";
        string temp = "";
        //순차 출력
        foreach (var t in text)
        {
            _dialogText.text = temp + "_";    //타자 연출
            temp += t;  //저장
            yield return new WaitForSeconds(_nextTextDelayTime);  //출력 딜레이
           
            if (_omission)
            {
                _omission = false;

                break;
            }
        }
        _dialogText.text = text;
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(FadeOut());
        yield return null;
    }
    IEnumerator FadeOut()
    {
        Color color = _dialogText.color;
        while (true)
        {
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

                    break;
                }
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                break;
            }
        }
        color.a = 0;
        _dialogText.color = color;
        foreach (var tmp in _fadeoutTxt)
        {
            tmp.color = color;
        }
        foreach (var img in _bgImg)
        {
            img.color = color;
        }
        yield return null;
    }
    public void PrintTxt(string text)
    {
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
        //순차 출력 Coroutine?
        StartCoroutine(SequentialPrint(text));
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
