using System.Collections;
using TMPro;
using UnityEngine;
public class HomeCtrlObj : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] _tmps;
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
            if (color.a <= 0)
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }

        //SceneManager
        SceneCtrlManager._instance.GoScene(SceneCtrlManager.SceneName.InGame);
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
    public void GotoRecord()
    {
        if (_clickBtn) return;
        StartCoroutine(GotoOtherScene());
    }
    public void EnterTMPColorSet(TextMeshProUGUI tmp)
    {
        Color color = Color.white;
        color.a = 0.5f;
        tmp.color = color;
    }
    public void ExitTMPColorSet(TextMeshProUGUI tmp)
    {
        Color color = Color.white;
        tmp.color = color;
    }
}
