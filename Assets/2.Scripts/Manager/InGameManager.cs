using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class InGameManager : TSingleTon<InGameManager>
{
    HintChecker _hintBox;
    DealerCtrlObj _dealerCtrlObj;
    ScoreCtrlObj _scoreCtrlObj;
    TextMeshProUGUI _timeTMP;

    List<NumBoxCtrlObj> _numBoxCtrlObjs;
    [SerializeField] int _result;
    int _curScore;
    [SerializeField] List<int> _binaryNum;
    [SerializeField] Dictionary<int, CardCtrlObj> _cardsDict;


    int[] _binarySetting;
    int _binaryCellCount = 8;
    float _curTime, _maxTime = 15.99f;
    InGameStatus _curStatus;

    public enum InGameStatus
    {
        SpreadCards,
        InGame,

        None
    }
    public int _curResult
    {
        get { return _result; }
    }
    public void setBinarySetting(int place, bool isOn = true)
    {
        if (isOn)
            _binarySetting[place] = 1;
        else
            _binarySetting[place] = 0;

        _result = 0;
        for (int i = 0; i < _binarySetting.Length; i++)
        {
            _result += _binarySetting[i] * (int)Mathf.Pow(2, i);
        }
        _hintBox.SetResult("" + _result);
    }
    public void AddCardList(CardCtrlObj co, bool reset = false)
    {
        if (reset)
        {
            _cardsDict = new Dictionary<int, CardCtrlObj>();
        }
        else
        {
            _cardsDict.Add(co._cardNum, co);
        }

    }
    public int GetNextRandomBinary()
    {
        int order = Random.Range(0, _binaryNum.Count);
        int result = _binaryNum[order];
        _binaryNum.Remove(result);
        return result;
    }
    public void SetBinaryNum()
    {
        _binaryNum = new List<int>();
        for (int i = 0; i < (int)Mathf.Pow(2, _binaryCellCount); i++)
        {
            _binaryNum.Add(i);
        }
    }
    public void FindResult()
    {
        CardCtrlObj co = null;
        RectTransform rt;
        if (_cardsDict.TryGetValue(_result, out co))
        {
            rt = co.GetComponent<RectTransform>();
            Debug.LogFormat("제출한 번호를 찾았습니다! : {0}", _result);
            _curScore += _result;
            _scoreCtrlObj.setText("" + _curScore);
            ResetNumCardNPad();
            _curTime = _maxTime;
        }
        else
        {
            Debug.Log("제출한 번호가 없습니다.");
        }
    }
    public void ResetNumCardNPad()
    {
        _dealerCtrlObj.RemoveCards();
        for (int i = 0; i < _binarySetting.Length; i++)
        {
            _binarySetting[i] = 0;
        }
        _result = 0;
        _hintBox.SetResult("0");
        foreach (NumBoxCtrlObj nbc in _numBoxCtrlObjs)
        {
            if (nbc._state == NumBoxCtrlObj.NumState.one)
            {
                nbc.NumReset();
            }
            nbc.isChanging = false;
        }
        _cardsDict = new Dictionary<int, CardCtrlObj>();



        //임시
        _dealerCtrlObj.TurnStart();
    }
    protected override void Init()
    {
        SetBinaryNum();
        _binarySetting = new int[_binaryCellCount];
        _result = 0;
        _curScore = 0;
        _curTime = _maxTime;
        _cardsDict = new Dictionary<int, CardCtrlObj>();
        _curStatus = InGameStatus.None;

        GameObject go = GameObject.FindGameObjectWithTag("HintBox");
        _hintBox = go.GetComponent<HintChecker>();
        go = GameObject.FindGameObjectWithTag("Dealer");
        _dealerCtrlObj = go.GetComponent<DealerCtrlObj>();
        go = GameObject.FindGameObjectWithTag("ScoreUI");
        _scoreCtrlObj = go.GetComponent<ScoreCtrlObj>();
        go = GameObject.FindGameObjectWithTag("TimeUI");
        _timeTMP = go.GetComponent<TextMeshProUGUI>();


        GameObject[] gos = new GameObject[_binaryCellCount];
        _numBoxCtrlObjs = new List<NumBoxCtrlObj>();
        gos = GameObject.FindGameObjectsWithTag("NumBox");
        foreach (GameObject g in gos)
        {
            _numBoxCtrlObjs.Add(g.GetComponent<NumBoxCtrlObj>());
        }
    }
    public void SetGameStatus(InGameStatus status)
    {
        _curStatus = status;
    }
    private void Update()
    {
        switch (_curStatus)
        {
            case InGameStatus.None:
                if(_binaryNum.Count < 3)
                {
                    //게임 종료
                    SceneCtrlManager._instance.GoScene(SceneCtrlManager.SceneName.Start);
                }
                break;
            case InGameStatus.SpreadCards:
                break;
            case InGameStatus.InGame:
                if (_curTime > 0)
                {
                    _curTime -= Time.deltaTime;
                    if (_curTime < 0)
                    {
                        _curTime = _maxTime;
                        ResetNumCardNPad();
                        SetGameStatus(InGameStatus.None);
                    }
                    _timeTMP.text = (int)_curTime + "";
                }
                break;

        }
    }
}
