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
        transform.GetChild(0).gameObject.SetActive(on);
    }
    public void SetSoundValue()
    {
        SoundManager._instance.SetBgmVolume(_bgmSlider.value);
        SoundManager._instance.SetSFXVolume(_sfxSlider.value);
    }
    public void GoHomeScnene()
    {
        SceneCtrlManager._instance.GoScene(SceneCtrlManager.SceneName.Start);
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
