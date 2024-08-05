using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainUIManager : MonoBehaviour
{
    static MainUIManager _uniqueInstance;

    [SerializeField] TextMeshProUGUI _message, _name, _id;
    [SerializeField] Image _profileImg;
    Image _messageLayer;
    private Texture2D curDownTex = null; private Texture2D texMe = null;

    public static MainUIManager _instance
    {
        get { return _uniqueInstance; }
    }

    void Awake()
    {
        _uniqueInstance = this;

        _message.text = string.Empty;
        _messageLayer = _message.transform.parent.GetComponent<Image>();
        _messageLayer.gameObject.SetActive(false);
    }
    public void ShowMessage(string msg)
    {
        _messageLayer.gameObject.SetActive(true);
        _message.text = msg;
    }
    public void ClickInitButton()
    {
        FBApp._instance.InitFacebook();
    }
    public void ClickLoginButton()
    {
        if (FBApp._instance._FBlog)
        {
            FBApp._instance.LogoutFacebook();
            _message.text = "LogOut";
        }
        else
        {
            FBApp._instance.LoginFacebook();
        }
    }

    internal void ShowViewer(string name, string id)
    {
        _name.text = name;
        _id.text = id;

        StartCoroutine(MyPicDown(id));
    }

    private IEnumerator MyPicDown(string strId) { yield return StartCoroutine(DownPic(strId)); texMe = curDownTex; }
    private IEnumerator DownPic(string strFacebookId)
    {
        string strPicUrl; strPicUrl = System.String.Format("http://graph.facebook.com/{0}/picture", strFacebookId);
        WWW www = new WWW(strPicUrl);
        Debug.Log(strPicUrl);
        yield return www;
        if (www.error != null) { Debug.Log("Picture error"); }
        else
        {
            curDownTex = www.texture;//www.LoadImageIntoTexture(curDownTex);
            Debug.Log(www.url);
            _profileImg.sprite = Sprite.Create( curDownTex, new Rect(0, 0, curDownTex.width, curDownTex.height), new Vector2(0.5f, 0.5f));
        }
    }
}
