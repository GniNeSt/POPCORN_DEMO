using UnityEngine;

public class GameManager : TSingleTon<GameManager>
{
    GameObject _settingPopUI;
    protected override void Init()
    {
        base.Init();
        if (_settingPopUI == null)
        {
            GameObject go = Resources.Load("Prefabs/SettingCanvas") as GameObject;
            _settingPopUI = Instantiate(go);
            DontDestroyOnLoad(_settingPopUI);
            _settingPopUI.SetActive(false);
        }
    }
    public void CheckPlayerSet()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _settingPopUI.SetActive(!_settingPopUI.activeSelf);
        }
    }
}
