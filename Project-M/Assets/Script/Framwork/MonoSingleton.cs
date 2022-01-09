using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {
    static T instance;
    //有时候，有的组件场景切换的时候回收
    public static bool destroyOnLoad = false;
    public static GameObject monoSingleton;
    public static T Instance {
        get {
            if (monoSingleton == null) {
                monoSingleton = new GameObject("monoSingleton");
                DontDestroyOnLoad(monoSingleton);
            }

            if (monoSingleton != null && instance == null)
                instance = monoSingleton.AddComponent<T>();

            return instance;
        }
    }

    public static void Init() {
        if (Instance == null) {
        
        }
    }

    //添加场景切换时候的事件
    public void AddSceneChangedEvent() {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene arg0,Scene arg1) {
        if (destroyOnLoad == true) {
            if (instance != null) {
                DestroyImmediate(instance);     //立即销毁
            }
        }
    }
}
