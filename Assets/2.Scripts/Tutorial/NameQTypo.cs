using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NameQTypo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _dialogText;
    [SerializeField] TextMeshProUGUI _endTMP;
    [SerializeField]float _nextTextDelayTime = 1f;
    [SerializeField] float _endTMPdelayTime = 1.0f;
    [SerializeField] GameObject _inputBoxObj;
    float _curTime = 0f;
    bool _finished = false;
    bool _typoFunc = false;
    IEnumerator SequentialPrint(string text)
    {
        _endTMP.text = "";
        transform.GetChild(0).gameObject.SetActive(true);
        _dialogText.color = Color.white;
        _dialogText.text = "_";
        string temp = "";
        //순차 출력
        foreach (var t in text)
        {
            _dialogText.text = temp + "_";    //타자 연출
            temp += t;  //저장
            SoundManager._instance.PlaySFX(SoundManager.SFXClipName.OldTypewriter);
            yield return new WaitForSeconds(_nextTextDelayTime);  //출력 딜레이

        }
        _dialogText.text = text;
        if(!_typoFunc)
            _endTMP.text = "_";
        _finished = true;
        yield return new WaitForSeconds(1.0f);
        _inputBoxObj.SetActive(true);
        yield return null;
    }
    public void PrintTypo(string str)
    {
        _typoFunc = true;
        StartCoroutine(SequentialPrint(str));
    }
    private void Awake()
    {
        StartCoroutine(SequentialPrint(_dialogText.text));
        _inputBoxObj.SetActive(false);
    }
    private void Update()
    {
        if (!_finished || _typoFunc) return;
        _curTime += Time.deltaTime;
        if(_curTime > _endTMPdelayTime)
        {
            _curTime = 0;
            Color color = _endTMP.color;

            color.a = color.a == 0 ? 1 : 0;

            _endTMP.color = color;
        }
    }
}
