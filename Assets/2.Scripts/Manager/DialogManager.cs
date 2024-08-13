using System;
using UnityEngine;
public class DialogManager : MonoBehaviour
{
    [SerializeField] DialogObj[] _dialogBox;
    [SerializeField] Dialog[] _dialogs;
    Dialog[] _basicDialogs;
    [SerializeField] Dialog[] _EndingDialogs;
    public enum DialogType
    {
        DialogCanvasUp,
        DialogCanvasDown,

        None
    }
    public enum DialogProperty
    {
        Start,
        InGame,

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
        foreach (var db in _dialogBox)
        {
            db.transform.GetChild(0).gameObject.SetActive(false);
        }
        _basicDialogs = new Dialog[4];
        InitDialog();
    }
    public void InitDialog()
    {
        _basicDialogs[0] =
            new Dialog(string.Format("{0}{1}{2}", "�Ǹ��մϴ�.\n��ſ��� ���� ��ȸ�� \"", InGameManager._instance._curHp,
                "\"�Դϴ�."));
        _basicDialogs[1] =
         new Dialog(string.Format("{0}{1}{2}", "�̷�, �Ǽ��ϼ̱���.\n���� ��ȸ�� \"", InGameManager._instance._curHp,
        "\"�Դϴ�."));
        _basicDialogs[2] = new Dialog(string.Format("{0}{1}{2}", "�̷�....\n��ȸ�� \"", InGameManager._instance._curHp,
        "\"�ۿ� ���� �ʾҽ��ϴ�."));
        _basicDialogs[3] = new Dialog("...�Ǹ��մϴ�.");
    }
    public void PrintDialog(int num, DialogProperty dP = DialogProperty.InGame)
    {
        switch (dP)
        {
            case DialogProperty.InGame:
                _dialogBox[(int)_basicDialogs[num]._type].PrintTxt(_basicDialogs[num]._txt);
                break;
            case DialogProperty.End:
                _dialogBox[(int)_EndingDialogs[num]._type].PrintTxt(_EndingDialogs[num]._txt);
                break;
            case DialogProperty.Start:
                _dialogBox[(int)_dialogs[num]._type].PrintTxt(_dialogs[num]._txt);
                break;
        }
    }
}
