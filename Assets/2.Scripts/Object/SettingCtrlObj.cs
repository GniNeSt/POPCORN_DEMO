using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SettingCtrlObj : MonoBehaviour
{
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _sfxSlider;
    [SerializeField] TextMeshProUGUI _nameTMP;
    [SerializeField] TextMeshProUGUI _scoreTMP;
    public void SwitchSettingUI(bool on = true)
    {
        gameObject.SetActive(on);
        _bgmSlider.value = PlayerPrefs.GetFloat("BGMValue");
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXValue");
        _nameTMP.text = SaveManager._instance._nameInfo + "의 최고기록";
        _scoreTMP.text = "" + SaveManager._instance._scoreInfo;
        Debug.LogFormat("{0},{1}",SaveManager._instance._nameInfo, SaveManager._instance._scoreInfo);
    }
    public void SetSoundValue()
    {
        SoundManager._instance.SetBgmVolume(_bgmSlider.value);
        SoundManager._instance.SetSFXVolume(_sfxSlider.value);
        SaveManager._instance.soundSave(_bgmSlider.value, _sfxSlider.value);
    }
    public void InitValue()
    {
        _bgmSlider.value = _sfxSlider.value = 0.5f;
        SetSoundValue();
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
