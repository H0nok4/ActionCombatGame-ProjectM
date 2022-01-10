using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {
    static T _Instance;
    //��ʱ���е���������л���ʱ�����
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
            // һЩ��Ҫ�ĳ�ʼ����Init����ִ��
            OnInitialize();
        }
        else {
            // �����ǰ�����ڳ��������Ѿ�����һ�������ˣ���ɵ���ǰ�����
            if (this != _Instance) {
                Destroy(this.gameObject);
            }
        }
    }

    //��ӳ����л�ʱ����¼�
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
                DestroyImmediate(_Instance);     //��������
            }
        }
    }
}
