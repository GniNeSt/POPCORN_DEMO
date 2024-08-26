using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InGameManager : TSingleTon<InGameManager>
{
    HintChecker _hintBox;
    DealerCtrlObj _dealerCtrlObj;
    ScoreCtrlObj _scoreCtrlObj;
    TextMeshProUGUI _timeTMP;

    DialogManager _dialogManager;
    ItemManager _itemManager;

    Image _endPanelImg;
    Image _hurtBGImg;

    List<NumBoxCtrlObj> _numBoxCtrlObjs;
    [SerializeField] int _result;
    int _curScore;
    [SerializeField] List<int> _binaryNum;
    [SerializeField] Dictionary<int, CardCtrlObj> _cardsDict;


    int[] _binarySetting;
    int _binaryCellCount = 8;
    float _curTime;
    float _maxTime = 15.99f;
    InGameStatus _curStatus;

    bool _itemFlag;
    bool _submitError;
    bool _spreadFlag;

    int _maxHp = 10;
    int _hpCount = 10;
    int _sceneNum = 0;
    int _targetScore = 666;

    int _itemEquipScore = 300;
    int _curItemPoint = 0;
    
    public int _needScore
    {
        get { return _targetScore; }
    }
    public int _btnRisk
    {
        get; set;
    }
    public int _errorRisk
    {
        get; set;
    }
    public bool _isBuffItem
    {
        get; set;
    }
    public int _curHp
    {
        get { return _hpCount; }
    }
    public InGameStatus _InGameStatus
    {
        get { return _curStatus; }
    }
    public enum InGameStatus
    {
        Start,
        SpreadCards,
        InGame,
        End,
        Item,


        None
    }
    public int _curResult
    {
        get { return _curScore; }
    }
    public bool _dialogClickEvent
    {
        get; set;
    }
    IEnumerator HurtBGStart()
    {
        Color color = _hurtBGImg.color;
        color.a = 0.15f;
        _hurtBGImg.color = color;
        while (true)
        {
            color.a -= 0.05f;
            _hurtBGImg.color = color;
            if (color.a <= 0)
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }
    public void SetSubmitBool(bool result)
    {
        _submitError = result;
    }
    public void PlusMaxTime(float time)
    {
        _maxTime += time;
    }
    public void RecoverHP(int i)
    {
        _hpCount += i;
        if (_hpCount >= _maxHp)
            _hpCount = _maxHp;
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
            _dialogManager.PrintDialog(0);
            _curScore += _result;

            _curItemPoint += _result;

            _scoreCtrlObj.setText("" + _curScore);
            ResetNumCardNPad();
            _curTime = _maxTime;


            SetGameStatus(InGameStatus.None);
        }
        else
        {
            Debug.Log("제출한 번호가 없습니다.");
            _submitError = true;
        }
    }
    public void ResetNumCardNPad()
    {
        bool _isChanged = false;
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
                _isChanged = nbc.NumReset();
            }
            nbc.isChanging = false;
        }
        _cardsDict = new Dictionary<int, CardCtrlObj>();
        if (_isChanged)
            SoundManager._instance.PlaySFX(SoundManager.SFXClipName.NumBoxReset);
    }
    protected override void Init()
    {
        base.Init();
        _dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();
        _endPanelImg = GameObject.FindGameObjectWithTag("EndPanel").transform.GetChild(0).GetComponent<Image>();
        _endPanelImg.gameObject.SetActive(false);

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
        _scoreCtrlObj.setText("0");
        go = GameObject.FindGameObjectWithTag("TimeUI");
        _timeTMP = go.GetComponent<TextMeshProUGUI>();
        _timeTMP.text = "" + (int)_maxTime;
        go = GameObject.FindGameObjectWithTag("ItemManager");
        _itemManager = go.GetComponent<ItemManager>();
        go = GameObject.FindGameObjectWithTag("HurtBG");
        _hurtBGImg = go.GetComponent<Image>();

        Color color = _hurtBGImg.color;
        color.a = 0;
        _hurtBGImg.color = color;

        GameObject[] gos = new GameObject[_binaryCellCount];
        _numBoxCtrlObjs = new List<NumBoxCtrlObj>();
        gos = GameObject.FindGameObjectsWithTag("NumBox");
        foreach (GameObject g in gos)
        {
            _numBoxCtrlObjs.Add(g.GetComponent<NumBoxCtrlObj>());
        }

        _btnRisk = 1;
        _errorRisk = 1;
        _maxHp = 10;
        if(SaveManager._instance._scoreInfo < 666)
        {
            _targetScore = 666;
            _itemEquipScore = 150;

        }
        else if(SaveManager._instance._scoreInfo < 999)
        {
            _targetScore = 999;
            _itemEquipScore = 300;
        }
        else if(SaveManager._instance._scoreInfo < 1500)
        {
            _targetScore = 1500;
            _itemEquipScore = 400;
        }
        else
        {
            _targetScore = 99999;
            _itemEquipScore = 400;
        }
    }
    public void SetGameStatus(InGameStatus status)
    {
        _curStatus = status;
    }
    public void DealerTurnStart()
    {
        _dealerCtrlObj.TurnStart();
    }
    public void PlayEndScene()
    {
        _curStatus = InGameStatus.End;
        _dialogManager.PrintDialog(0, DialogManager.DialogProperty.End, true);
    }
    private void Update()
    {
        switch (_curStatus)
        {
            case InGameStatus.None:
                if (_binaryNum.Count < 3)
                {
                    //게임 종료
                    SceneCtrlManager._instance.GoScene(SceneCtrlManager.SceneName.Start);
                }
                if (_curScore >= _targetScore)
                {
                    _dialogManager.PrintDialog(3);
                    PlayEndScene();
                }
                else
                {
                    if (_curItemPoint >= _itemEquipScore)  ///////아이템 획득 조건////////////////////////
                    {   //300이상
                        _curItemPoint = 0;
                        _itemFlag = false;
                        _isBuffItem = true;
                        _curStatus = InGameStatus.Item;
                    }
                    else
                    {
                        SetGameStatus(InGameStatus.SpreadCards);
                    }

                }
                break;
            case InGameStatus.Start:
                if (_sceneNum >= 2)
                {
                    _curTime -= Time.deltaTime;
                    if (_curTime <= _maxTime - 3)
                    {
                        _curTime = _maxTime;
                        _sceneNum = 0;
                        SetGameStatus(InGameStatus.SpreadCards);
                    }
                    break;
                }
                else if (_dialogClickEvent)
                {
                    _dialogManager.PrintDialog(_sceneNum++, DialogManager.DialogProperty.Start, true);
                    _dialogClickEvent = false;
                }
                break;
            case InGameStatus.SpreadCards:
                if (!_spreadFlag)
                {
                    _curTime -= Time.deltaTime;
                    if (_curTime <= _maxTime - 2)
                    {
                        _itemFlag = false;
                        _dealerCtrlObj.TurnStart();
                        _curTime = _maxTime;
                        _spreadFlag = true;
                    }
                }
                break;
            case InGameStatus.InGame:
                _spreadFlag = false;
                if (_curTime > 0)
                {
                    _curTime -= Time.deltaTime;
                    if (_curTime < 0 || _submitError)
                    {
                        _submitError = false;
                        SoundManager._instance.PlaySFX(SoundManager.SFXClipName.Locked);
                        StartCoroutine(HurtBGStart());
                        _hpCount -= _errorRisk;
                        if (_hpCount <= 0)
                        {
                            PlayEndScene();
                        }
                        else
                        {
                            _dialogManager.PrintDialog(1);
                            _curTime = _maxTime;
                            ResetNumCardNPad();
                            if (_curStatus != InGameStatus.End)
                                SetGameStatus(InGameStatus.None);
                        }
                    }
                    _timeTMP.text = (int)_curTime + "";
                }
                break;
            case InGameStatus.End:
                SaveManager._instance.Save();
                _endPanelImg.gameObject.SetActive(true);
                if (_endPanelImg.color.a < 1f)
                {
                    Color color = _endPanelImg.color;
                    color.a += Time.deltaTime * 2f;
                    _endPanelImg.color = color;
                }
                if (_dialogClickEvent)
                {
                    if (_sceneNum > 3)
                    {
                        SceneCtrlManager._instance.GoScene(SceneCtrlManager.SceneName.Start);
                        break;
                    }
                    _dialogManager.PrintDialog(_sceneNum++, DialogManager.DialogProperty.End, true);
                    _dialogClickEvent = false;

                }
                break;
            case InGameStatus.Item:
                if (!_itemFlag)
                {
                    ResetNumCardNPad();
                    _itemFlag = true;
                    _itemManager.SetItem(_isBuffItem);
                }
                break;
        }
    }
}
