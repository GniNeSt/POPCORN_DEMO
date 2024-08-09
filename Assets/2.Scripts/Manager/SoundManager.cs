using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : TSingleTon<SoundManager>
{
    public enum BGMClipName
    {
        Start,
        Normal1,
        Normal2,
        Hard,
        GameOver,

        max
    }
    public enum SFXClipName
    {
        Dot,
        Patterned,
        Locked,
        Heal,
        Hurt,

        max
    }
    Dictionary<BGMClipName, AudioClip> _bgmClipList;
    Dictionary<SFXClipName, AudioClip> _sfxClipList;

    AudioSource _bgmPlayer;
    AudioSource _sfxPlayer;
    protected override void Init()
    {
        base.Init();
        GameObject go = new GameObject("BGM", typeof(AudioSource));
        go.transform.parent = transform;
        go = new GameObject("SFX", typeof(AudioSource));
        go.transform.parent = transform;
        LoadClips();

        PlayBGM(BGMClipName.Start);
    }
    void LoadClips()
    {
        int count = (int)BGMClipName.max;
        _bgmClipList = new Dictionary<BGMClipName, AudioClip> ();
        string path = "Sounds/BGM/";
        for(int n = 0; n < count; n++)
        {
            string name = path + ((BGMClipName)n).ToString();
            AudioClip ac = Resources.Load<AudioClip>(name);
            _bgmClipList.Add((BGMClipName)n, ac);
        }

        count = (int)SFXClipName.max;
        _sfxClipList = new Dictionary<SFXClipName, AudioClip>();
        path = "Sounds/SFX/";
        for (int n = 0; n < count; n++)
        {
            string name = path + ((SFXClipName)n).ToString();
            Debug.Log(name);
            AudioClip ac = Resources.Load<AudioClip>(name);
            Debug.Log(ac);
            _sfxClipList.Add((SFXClipName)n, ac);
        }

        _bgmPlayer = transform.GetChild(0).GetComponent<AudioSource>();
        _sfxPlayer = transform.GetChild(1).GetComponent<AudioSource>();
    }

    public void PlayBGM(BGMClipName name, bool isLoop = true)
    {
        if(_bgmPlayer != null)
        {
            _bgmPlayer.clip = _bgmClipList[name];
            //_bgmPlayer.volume = UserInfoManager._instance._bgmVolume;
            //_bgmPlayer.mute = UserInfoManager._instance._bgmMute;
            _bgmPlayer.loop = isLoop;

            _bgmPlayer.Play();
        }
    }

    public void PlaySFX(SFXClipName name, bool isLoop = false)
    {
        if(_sfxPlayer != null)
        {
            _sfxPlayer.clip = _sfxClipList[name];
            //_sfxPlayer.volume = UserInfoManager._instance._sfxVolume;
            //_sfxPlayer.mute = UserInfoManager._instance._sfxMute;
            _sfxPlayer.loop = isLoop;

            _sfxPlayer.PlayOneShot(_sfxClipList[name]);
        }
    }
    public void SetBgmVolume(float volume)
    {
        _bgmPlayer.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        _sfxPlayer.volume = volume;
    }
}
