using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {
    static T _Instance;
    //有时候，有的组件场景切换的时候回收
    public static bool destroyOnLoad = false;
    public static GameObject monoSingleton;
    public static T Instance {
        get {
            if (_Instance == null) {
                var singletonObj = new GameObject("@" + typeof(T).Name);
                singletonObj.AddComponent<T>();
            }
            return _Instance;
        }
    }

    public static void CreatInstance() {
        var ins = Instance;
    }

    private void Awake() {
        if (_Instance == null) {
            _Instance = this as T;
            DontDestroyOnLoad(this.gameObject);
            // 一些必要的初始化在Init里面执行
            OnInitialize();
        }
        else {
            // 如果当前对象在场景里面已经存在一个引用了，则干掉当前这个。
            if (this != _Instance) {
                Destroy(this.gameObject);
            }
        }
    }

    //添加场景切换时候的事件
    public void AddSceneChangedEvent() {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    public virtual void OnInitialize() {

    }

    public virtual void OnUnInitialize() {

    }

    private void OnSceneChanged(Scene arg0,Scene arg1) {
        if (destroyOnLoad == true) {
            if (_Instance != null) {
                DestroyImmediate(_Instance);     //立即销毁
            }
        }
    }
}
