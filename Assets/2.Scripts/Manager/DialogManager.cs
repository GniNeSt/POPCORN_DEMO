using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogManager : MonoBehaviour
{
    [SerializeField]Dialog[] _dialogs;
    [SerializeField] Dialog[] _basicDialogs;
    DialogObj[] _dialogBox;
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
    }
    private void Awake()
    {
        DialogObj DO = null;
        for (int i =0; i<(int)DialogType.None; i++)
        {
            string path = "Prefabs/" + string.Format("{0}", ((DialogType)i));
            DO = Resources.Load(path) as DialogObj;
            _dialogBox[i] = Instantiate(DO);
            _dialogBox[i].InitDalog();
        }
        
    }
    public void PrintDialog(int num)
    {        
        _dialogBox[(int)_dialogs[num]._type].PrintTxt(_dialogs[num]._txt);
    }
}
