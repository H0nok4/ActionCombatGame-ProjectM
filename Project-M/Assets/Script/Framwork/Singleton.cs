using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class,new ()
{
    private static T _instance;

    public static T Instance {
        get {
            if (_instance == null) {
                _instance = new T();
            }

            return _instance;
        }
    }

    protected Singleton() {
        if (null != _instance) {
            Debug.LogError($"单例初始化不为Null,名称为{typeof(T).ToString()}");
        }
        Init();
        
    }

    public virtual void Init() {
        
    }
}
