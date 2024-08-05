using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FBApp : MonoBehaviour
{
    static FBApp _uniqueInstance;
    [SerializeField] GameObject _sdkBtn;

    string _name, _id;


    public static FBApp _instance
    {
        get { return _uniqueInstance; }
    }
    public bool _FBlog
    {
        get { return FB.IsLoggedIn; }
    }
    private void Awake()
    {
        _uniqueInstance = this;
    }
    //�ʱ�ȭ �Լ� �����, ȣ��Ǵ� �Լ��� ������ �ʱ�ȭ�κ�
    void InitCallBack()
    {
        if (FB.IsInitialized)
        {
            Debug.Log("���̽��� SDK �ʱ�ȭ �Ϸ�");


            //�ʱ�ȭ ��ư ��Ȱ��ȭ
            _sdkBtn.SetActive(false);
            FB.ActivateApp();
        }
        else
            Debug.Log("���̽��� SDK �ʱ�ȭ ����");
    }
    //InitCallBAck �Լ��� ����Ǳ� ����, ���� ȣ��Ǵ� �Լ�
    //���̽��� �α��� ���μ��� ���� ���߿� ����Ƽ�� ���߰� �ϴ� ���
    void OnHideUnity(bool isGameShow)
    {
        if (isGameShow)
        {
            Time.timeScale = 1;
        }
        else
            Time.timeScale = 0;
    }
    void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("���̽��� �α��� �Ϸ�");
            AccessToken token = AccessToken.CurrentAccessToken;

            FB.API("/me?fields=id,name", HttpMethod.GET, GetUserIInfoCallback);

            MainUIManager._instance.ShowMessage(token.UserId);
        }
        else
        {
            if (result.Error != null)
                Debug.LogFormat("���̽��� �α��� ����(ERROR CODE : {0})", result.Error);
            else
                Debug.Log("���̽��� �α��� ���� (User Cancled)");
        }
    }
    public void InitFacebook()
    {
        if (!FB.IsInitialized)
        {
            Debug.Log("���̽��� SDK �ʱ�ȭ ����");
            FB.Init(InitCallBack, OnHideUnity);
        }
        else
        {
            Debug.Log("���̽��� SDK �ʱ�ȭ �Ϸ�");
            FB.ActivateApp();
        }
    }
    public void LoginFacebook()
    {
        List<string> parms = new List<string>() { "public_profile", "email" };
        FB.LogInWithPublishPermissions(parms, AuthCallback);
    }
    public void LogoutFacebook()
    {
        FB.LogOut();
    }

    void GetUserIInfoCallback(IResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("���� ���� �б� ����");
            return;
        }
        Dictionary<string, object> userInfo = Facebook.MiniJSON.Json.Deserialize(result.RawResult) as Dictionary<string, object>;
        if (userInfo == null)
            Debug.LogError("���� ������ �Ľ̿� �����Ͽ����ϴ�. ���� : " + result.RawResult);
        if (userInfo["name"] != null)
            _name = userInfo["name"].ToString();
        if (userInfo["id"] != null)
            _id = userInfo["id"].ToString();

        MainUIManager._instance.ShowViewer(_name, _id);
    }
}
