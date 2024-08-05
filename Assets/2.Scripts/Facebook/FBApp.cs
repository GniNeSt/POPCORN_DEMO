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
    //초기화 함수 실행뒤, 호출되는 함수로 실질적 초기화부분
    void InitCallBack()
    {
        if (FB.IsInitialized)
        {
            Debug.Log("페이스북 SDK 초기화 완료");


            //초기화 버튼 비활성화
            _sdkBtn.SetActive(false);
            FB.ActivateApp();
        }
        else
            Debug.Log("페이스북 SDK 초기화 실패");
    }
    //InitCallBAck 함수가 실행되기 이전, 이후 호출되는 함수
    //페이스북 로그인 프로세스 실행 도중에 유니티를 머추게 하는 기능
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
            Debug.Log("페이스북 로그인 완료");
            AccessToken token = AccessToken.CurrentAccessToken;

            FB.API("/me?fields=id,name", HttpMethod.GET, GetUserIInfoCallback);

            MainUIManager._instance.ShowMessage(token.UserId);
        }
        else
        {
            if (result.Error != null)
                Debug.LogFormat("페이스북 로그인 실패(ERROR CODE : {0})", result.Error);
            else
                Debug.Log("페이스북 로그인 실패 (User Cancled)");
        }
    }
    public void InitFacebook()
    {
        if (!FB.IsInitialized)
        {
            Debug.Log("페이스북 SDK 초기화 시작");
            FB.Init(InitCallBack, OnHideUnity);
        }
        else
        {
            Debug.Log("페이스북 SDK 초기화 완료");
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
            Debug.Log("유저 정보 읽기 실패");
            return;
        }
        Dictionary<string, object> userInfo = Facebook.MiniJSON.Json.Deserialize(result.RawResult) as Dictionary<string, object>;
        if (userInfo == null)
            Debug.LogError("유저 정보의 파싱에 실패하였습니다. 이유 : " + result.RawResult);
        if (userInfo["name"] != null)
            _name = userInfo["name"].ToString();
        if (userInfo["id"] != null)
            _id = userInfo["id"].ToString();

        MainUIManager._instance.ShowViewer(_name, _id);
    }
}
