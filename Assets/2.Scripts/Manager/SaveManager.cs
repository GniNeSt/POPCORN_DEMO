using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : TSingleTon<SaveManager>
{
    string _name;
    int _maxScore;
    int _userListCount;
    int _userRank;
    //const string userList = "userList";

    public string _nameInfo
    {
        get { return _name; }
    }
    public int _scoreInfo
    {
        get { return _maxScore; }
    }
    protected override void Init()
    {
        base.Init();
        //int num = 0;
        //while (true)
        //{
        //    if(!PlayerPrefs.HasKey(userList + num))
        //    {
        //        _userListCount = 0;
        //        return;
        //    }
        //    else
        //    {
        //        num++;
        //    }
        //}
        //_userListCount = num;

    }
    public bool Load(string userName)
    {
        PlayerPrefs.SetString("LastPlayer", userName);
        if (!PlayerPrefs.HasKey("BGMValue"))
        {
            PlayerPrefs.SetFloat("BGMValue", 0.5f);
        }
        if (!PlayerPrefs.HasKey("SFXValue"))
        {
            PlayerPrefs.SetFloat("BGMValue", 0.5f);
        }

        if (PlayerPrefs.HasKey(userName))
        {
            Debug.Log("이미 있는 유저 정보");
            _name = userName;
            _maxScore = PlayerPrefs.GetInt(userName + "Score");
            _userRank = PlayerPrefs.GetInt(userName);
            return true;
        }
        else
        {
            Debug.Log("새로운 유저 정보");
            PlayerPrefs.SetInt(userName, 999);
            
            _userListCount++;
            PlayerPrefs.SetInt(userName + "Score", 0);
            _name = userName;
            _maxScore = PlayerPrefs.GetInt(userName + "Score");
            _userRank = PlayerPrefs.GetInt(userName);
            return false;
        }
    }
    public void Save()
    {
        if (PlayerPrefs.HasKey(_name))
        {
            int curScore = InGameManager._instance._curResult;
            Debug.Log(curScore);
            if (curScore > _maxScore)
            {
                Debug.Log("기록 갱신!!");
                PlayerPrefs.SetInt(_name + "Score", curScore);
                _maxScore = curScore;
            }
        }
    }
    public void soundSave(float bgm, float sfx)
    {
        PlayerPrefs.SetFloat("BGMValue", bgm);
        PlayerPrefs.SetFloat("SFXValue", sfx);
    }
}
