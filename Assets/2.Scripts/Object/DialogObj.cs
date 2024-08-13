using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogObj : MonoBehaviour//PopUpUI Load�� Init�Լ��� ȣ������!!!
{
    //����
    [SerializeField] TMP_Text _dialogText;
    [SerializeField] TMP_Text[] _fadeoutTxt;
    [SerializeField] float _nextTextDelayTime = 0.1f;
    Canvas _canvas;
    [SerializeField] Image[] _bgImg;
    //����
    bool _omission;//���� Bool
    bool _newOrder;
    float _curtime;
    IEnumerator SequentialPrint(string text)
    {
        while (_newOrder)
        {
            yield return new WaitForSeconds(_nextTextDelayTime);
        }
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
        //�ʱ�ȭ
        _omission = false;
        _dialogText.text = "_";
        string temp = "";
        //���� ���
        foreach (var t in text)
        {
            _dialogText.text = temp + "_";    //Ÿ�� ����
            temp += t;  //����
            yield return new WaitForSeconds(_nextTextDelayTime);  //��� ������
           
            if (_omission)
            {
                _omission = false;

                break;
            }
        }
        _dialogText.text = text;
        yield return new WaitForSeconds(1.0f);
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
                }
            }
        }
    }
    public void PrintTxt(string text)
    {
        _curtime = 0;
        //���� ��� Coroutine?
        StartCoroutine(SequentialPrint(text));
    }
    public void InitDalog()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _dialogText.text = "���� :\n�ؽ�Ʈ�� �ʱ�ȭ ���� �ʾҽ��ϴ�.";
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
    //        //PrintTxt("�ȳ��ϼ���! ���ο� ���\n����Դϴ�!!!");
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
