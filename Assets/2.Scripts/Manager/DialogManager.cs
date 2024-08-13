using System;
using UnityEngine;
public class DialogManager : MonoBehaviour
{
    [SerializeField] DialogObj[] _dialogBox;
    [SerializeField] Dialog[] _dialogs;
    Dialog[] _basicDialogs;
    public enum DialogType
    {
        DialogCanvasUp,
        DialogCanvasDown,

        None
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
        foreach(var db in _dialogBox)
        {
            db.transform.GetChild(0).gameObject.SetActive(false);
        }
        _basicDialogs = new Dialog[4];
        _basicDialogs[0] =
            new Dialog(string.Format("{0}{1}{2}", "훌륭합니다.\n당신에게 남은 기회는 \"", InGameManager._instance._curHp,
                "\"입니다."));
        _basicDialogs[1] =
         new Dialog(string.Format("{0}{1}{2}", "이런, 실수하셨군요.\n남은 기회는 \"", InGameManager._instance._curHp,
        "\"입니다."));
        _basicDialogs[2] = new Dialog(string.Format("{0}{1}{2}", "이런....\n기회가 \"", InGameManager._instance._curHp,
        "\"밖에 남지 않았습니다."));
        _basicDialogs[3] = new Dialog("...훌륭합니다.");
    }
    public void PrintDialog(int num, bool isBasic = true)
    {
        if (isBasic)
        {
            _dialogBox[(int)_basicDialogs[num]._type].PrintTxt(_basicDialogs[num]._txt);

        }
        else
        {
            _dialogBox[(int)_dialogs[num]._type].PrintTxt(_dialogs[num]._txt);

        }
    }
}
