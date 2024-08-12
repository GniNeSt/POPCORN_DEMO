using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingCtrlObj : MonoBehaviour
{
    [SerializeField]Slider _bgmSlider;
    [SerializeField]Slider _sfxSlider;
    public void SwitchSettingUI(bool on = true)
    {
        gameObject.SetActive(on);
    }
    public void SetSoundValue()
    {
        SoundManager._instance.SetBgmVolume(_bgmSlider.value);
        SoundManager._instance.SetSFXVolume(_sfxSlider.value);
        
        //초기화로 바꿀예정
    }
    public void GoHomeScnene()
    {
        SceneCtrlManager._instance.GoScene(SceneCtrlManager.SceneName.Start);
        SwitchSettingUI(false);
    }

    public void EnterColorSet(TextMeshProUGUI tmp)
    {
        Color color = Color.white;
        color.a = 0.5f;
        tmp.color = color;
        tmp.transform.parent.GetComponent<Image>().color = color;
    }
    public void ExitColorSet(TextMeshProUGUI tmp)
    {
        Color color = Color.white;
        tmp.color = color;
        tmp.transform.parent.GetComponent<Image>().color = color;
    }

}
