using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneCtrlManager : TSingleTon<SceneCtrlManager>
{
    public enum SceneName
    {
        Start,
        InGame,
        Tutorial
    }
    public void GoScene(SceneName sc)
    {
        switch (sc)
        {
            case SceneName.Start:
                Destroy(InGameManager._instance.gameObject);
                break;
        }
        SceneManager.LoadScene((int)sc);

        SoundManager._instance.PlayBGM((SoundManager.BGMClipName)(sc));
    }
}
