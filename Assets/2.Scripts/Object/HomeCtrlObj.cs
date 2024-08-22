using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HomeCtrlObj : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] _tmps;
    [SerializeField] Image[] _imgs;
    bool _clickBtn;
    IEnumerator GotoOtherScene()
    {
        _clickBtn = true;
        Color color = Color.white;
        while (true)
        {
            color.a -= 0.05f;
            foreach (var tmp in _tmps)
            {                
                tmp.color = color;
            }
            foreach(var img in _imgs)
            {
                img.color = color;
            }
            if (color.a <= 0)
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }

        //SceneManager
        SceneCtrlManager._instance.GoScene(SceneCtrlManager.SceneName.Tutorial);
        yield return null;
    }
    IEnumerator StartScene()
    {
        _clickBtn = true;
        Color color = Color.white;
        color.a = 0;
        while (true)
        {
            color.a += 0.05f;
            foreach (var tmp in _tmps)
            {
                tmp.color = color;
            }
            if (color.a >= 1)
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        _clickBtn = false;

        yield return null;
    }
    private void Awake()
    {
        StartCoroutine(StartScene());
    }
    public void GotoIngame()
    {
        if (_clickBtn) return;
        StartCoroutine(GotoOtherScene());
    }
    public void ExitGame()
    {
        if (_clickBtn) return;
        Application.Quit();
    }
    public void EnterTMPColorSet(TextMeshProUGUI tmp)
    {
        Color color = Color.white;
        color.a = 0.5f;
        tmp.color = color;
        tmp.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void ExitTMPColorSet(TextMeshProUGUI tmp)
    {
        Color color = Color.white;
        tmp.color = color;
        tmp.transform.GetChild(0).gameObject.SetActive(false);
    }
}
