using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TutorialLoadObj : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nameValue;
    [SerializeField] Image _panelImg;
    NameQTypo _nqt;
    bool _isClicked;
    IEnumerator GotoInGame()
    {
        yield return new WaitForSeconds(5.0f);
        _panelImg.gameObject.SetActive(true);
        Color color = _panelImg.color;
        color.a = 0;
        while (true)
        {
            color.a += 0.05f;
            _panelImg.color = color;
            if (color.a >= 1)
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }

        //SceneManager
        SceneCtrlManager._instance.GoScene(SceneCtrlManager.SceneName.InGame);
        yield return null;
    }
    private void Awake()
    {
        _panelImg.gameObject.SetActive(false);
        _nqt = GetComponent<NameQTypo>();
    }
    public void LoadFromName()
    {
        if (_isClicked) return;
        _isClicked = true;
        string dialogTypo = null;
        if (SaveManager._instance.Load(_nameValue.text))
            dialogTypo = string.Format("�ݰ��� �̸��̱���.\n��ٸ��� �־����ϴ�. {0}.", _nameValue.text);
        else
            dialogTypo = string.Format("�ݰ����ϴ�.\n{0}.", _nameValue.text);
        _nqt.PrintTypo(dialogTypo);

        StartCoroutine(GotoInGame());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadFromName();
        }
    }
}
