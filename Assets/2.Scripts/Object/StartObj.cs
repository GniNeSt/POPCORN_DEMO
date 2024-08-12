using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartObj : MonoBehaviour
{
    private void Awake()
    {
        GameManager._instance.CheckPlayerSet();

        SoundManager._instance.PlayBGM(SoundManager.BGMClipName.Start);
    }
}
