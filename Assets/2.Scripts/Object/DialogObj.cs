using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogObj : MonoBehaviour//PopUpUI Load�� Init�Լ��� ȣ������!!!
{   
    //����
    [SerializeField]TMP_Text _dialogText;
    [SerializeField] float _nextTextDelayTime = 0.1f;
    Canvas _canvas;
    //����
    bool _omission;//���� Bool
    IEnumerator SequentialPrint(string text)
    {
        //�ʱ�ȭ
        _omission = false;
        _dialogText.text = "_";
        string temp = "";
        //���� ���
        foreach(var t in text){
            _dialogText.text = temp+"_";    //Ÿ�� ����
            temp += t;  //����
            yield return new WaitForSeconds(_nextTextDelayTime);  //��� ������
            if (_omission)
            {
                _omission = false;

                break;
            }
        }
        _dialogText.text = text;
        yield return null;
    }
    public void PrintTxt(string text)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        //���� ��� Coroutine?
        StartCoroutine(SequentialPrint(text));
    }
    public void InitDalog()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _dialogText.text = "���� :\n�ؽ�Ʈ�� �ʱ�ȭ ���� �ʾҽ��ϴ�.";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _omission=true;
        }
    }
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle("button");
        style.fontSize = 36;

        if (GUI.Button(new Rect(120, 0, 120, 40), "Print", style))
        {
            //PrintTxt("�ȳ��ϼ���! ���ο� ���\n����Դϴ�!!!");
            if (transform.GetChild(0).gameObject.activeSelf)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                return;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                PrintTxt("Nice to meet you.\nWelcome to the popcorn game.");
            }
        }
    }
}
