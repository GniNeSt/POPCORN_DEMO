using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneCtrlManager : TSingleTon<SceneCtrlManager>
{
    public enum SceneName
    {
        Start,
        InGame
    }
    public void GoScene(SceneName sc)
    {
        SceneManager.LoadScene((int)sc);

        SoundManager._instance.PlayBGM((SoundManager.BGMClipName)(sc));
    }
}
