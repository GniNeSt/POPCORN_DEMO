using System;
using UnityEngine;
public class DialogManager : MonoBehaviour
{
    [SerializeField] DialogObj[] _dialogBox;
    [SerializeField] Dialog[] _dialogs;
    Dialog[] _basicDialogs;
    [SerializeField] Dialog[] _StartDialogs;
    [SerializeField] Dialog[] _EndingDialogs;
    [SerializeField] Dialog[] _ItemEquipDialogs;
    InGameManager _inGameManager;
    public enum DialogType
    {
        DialogCanvasUp,
        DialogCanvasDown,

        None
    }
    public enum DialogProperty
    {
        Tutorial,
        Start,
        InGame,
        Item,

        End
    }
    [Serializable]
    public class Dialog
    {
        [SerializeField] DialogType _dialogType;
        [TextArea] [SerializeField] string _text;
        public DialogType _type
        {
            get { return _dialogType; }
        }
        public string _txt
        {
            get { return _text; }
        }
        public Dialog(string text, DialogType type = DialogType.DialogCanvasUp)
        {
            _dialogType = type;
            _text = text;
        }
    }
    private void Awake()
    {
        //for (int i = 0; i < (int)DialogType.None; i++)
        //{
        //    string path = "Prefabs/" + string.Format("{0}", ((DialogType)i));
        //    Debug.Log(path);
        //    GameObject go = Instantiate(Resources.Load(path) as GameObject);
        //    _dialogBox[i] = go.GetComponent<DialogObj>();
        //    _dialogBox[i].InitDalog();
        //}
        _inGameManager = InGameManager._instance;

        foreach (var db in _dialogBox)
        {
            db.transform.GetChild(0).gameObject.SetActive(false);
        }
        _basicDialogs = new Dialog[4];
        InitDialog();
    }
    public void InitDialog()
    {
        if (_inGameManager._needScore >= 99999)
        {
            _StartDialogs[1] =
            new Dialog("이번 목표 점수는 없습니다. \n최대한 많은 점수를 얻으십시오.");
        }
        else
        {
            _StartDialogs[1] =
                new Dialog(string.Format("{0}{1}{2}{3}{4}", "이번 목표점수는 \"", _inGameManager._needScore,
                    "\",\n기회는 \"", InGameManager._instance._curHp, "\"입니다."));
        }
        _basicDialogs[0] =
            new Dialog(string.Format("{0}{1}{2}", "훌륭합니다.\n당신에게 남은 기회는 \"", _inGameManager._curHp,
                "\"입니다."));
        _basicDialogs[1] =
         new Dialog(string.Format("{0}{1}{2}", "이런, 실수하셨군요.\n남은 기회는 \"", _inGameManager._curHp,
        "\"입니다."));
        _basicDialogs[2] = new Dialog(string.Format("{0}{1}{2}", "이런....\n기회가 \"", _inGameManager._curHp,
        "\"밖에 남지 않았습니다."));
        _basicDialogs[3] = new Dialog("...훌륭합니다.");
    }
    public void PrintDialog(int num, DialogProperty dP = DialogProperty.InGame, bool wait = false)
    {
        InitDialog();
        switch (dP)
        {
            case DialogProperty.InGame:
                _dialogBox[(int)_basicDialogs[num]._type].PrintTxt(_basicDialogs[num]._txt, wait);
                break;
            case DialogProperty.End:
                _dialogBox[(int)_EndingDialogs[num]._type].PrintTxt(_EndingDialogs[num]._txt, wait);
                break;
            case DialogProperty.Start:
                _dialogBox[(int)_StartDialogs[num]._type].PrintTxt(_StartDialogs[num]._txt, wait);
                break;
            case DialogProperty.Tutorial:
                _dialogBox[(int)_dialogs[num]._type].PrintTxt(_dialogs[num]._txt, wait);
                break;
            case DialogProperty.Item:
                _dialogBox[(int)_ItemEquipDialogs[num]._type].PrintTxt(_ItemEquipDialogs[num]._txt);
                break;
        }
    }
}
