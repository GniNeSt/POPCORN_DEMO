using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TSingleTon<T> : MonoBehaviour where T : TSingleTon<T>
{
    static volatile T _uniqueInstance = null;
    static volatile GameObject _uniqueObject = null;

    protected TSingleTon()
    {
    }

    public static T _instance
    {
        get
        {
            if(_uniqueInstance == null)
            {
                lock (typeof(T))
                {
                    if(_uniqueInstance == null && _uniqueObject == null)
                    {// 체크
                        _uniqueObject = new GameObject(typeof(T).Name, typeof(T));
                        _uniqueInstance = _uniqueObject.GetComponent<T>();

                        _uniqueInstance.Init();
                    }
                }
            }
            return _uniqueInstance;
        }
    }

    protected virtual void Init()
    {
        DontDestroyOnLoad(this);
    }
}

