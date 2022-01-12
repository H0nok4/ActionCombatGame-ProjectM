using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using System;

public class UnityTask : INotifyCompletion {
    /// <summary>
    /// ���ҽ���Э��������ʱ����true(Э����ͣ����Ȼ��Ϊ�Ǵ���Runnig״̬)
    /// </summary>
    public bool Running => _Task.Running;

    public bool IsCompleted => !Running;

    public void GetResult() {
    }

    /// <summary>
    /// ���ҽ���Э�̴�����ͣ״̬ʱ������True
    /// </summary>
    public bool Paused => _Task.Paused;

    /// ���ҽ���Э���Ǳ��ֶ�����Stop�ӿ�ֹͣ�ǣ�manaulָΪtrue
    public delegate void FinishedHandler(bool manual);

    /// <summary>
    /// Э�̽����¼���Э��ִ����Ϻ󴥷����¼�
    /// </summary>
    public event FinishedHandler Finished;


    TaskManager.TaskState _Task;
    Action _Continuation;

    /// <summary>
    /// Ϊָ����Э�̴���һ��Task���󣬵�autoStartΪtrue��Ĭ��Ϊtrue����ʱ��
    /// task�ڹ���������Զ���ʼ
    /// </summary>
    /// <param name="c"></param>
    /// <param name="autoStart">�Զ���ʼ</param>
    public UnityTask(IEnumerator c, bool autoStart = true) {
        _Task = TaskManager.CreateTask(c);
        _Task.Finished += TaskFinished;
        if (autoStart)
            Start();
    }

    /// <summary>
    /// ��ʼִ��Э��
    /// </summary>
    public void Start() {
        _Task.Start();
    }

    /// <summary>
    /// ��Э�̵���һ��yieldʱ��ֹЭ��
    /// </summary>
    public void Stop() {
        _Task.Stop();
    }

    public void Pause() {
        _Task.Pause();
    }

    public void Unpause() {
        _Task.Unpause();
    }

    void TaskFinished(bool manual) {
        FinishedHandler handler = Finished;
        handler?.Invoke(manual);
        _Continuation?.Invoke();
    }

    public void OnCompleted(Action continuation) {
        _Continuation = continuation;
    }

    public UnityTask GetAwaiter() {
        return this;
    }

}

class TaskManager : MonoBehaviour {
    public class TaskState {
        public bool Running {
            get {
                return _Running;
            }
        }

        public bool Paused {
            get {
                return _Paused;
            }
        }

        public delegate void FinishedHandler(bool manual);
        public event FinishedHandler Finished;

        IEnumerator _Coroutine;
        bool _Running;
        bool _Paused;
        bool _Stopped;

        public TaskState(IEnumerator c) {
            _Coroutine = c;
        }

        public void Pause() {
            _Paused = true;
        }

        public void Unpause() {
            _Paused = false;
        }

        public void Start() {
            _Running = true;
            _Singleton.StartCoroutine(CallWrapper());
        }

        public void Stop() {
            _Stopped = true;
            _Running = false;
        }

        IEnumerator CallWrapper() {
            //yield return null;
            IEnumerator e = _Coroutine;
            while (_Running) {
                if (_Paused)
                    yield return null;
                else {
                    if (e != null && e.MoveNext()) {
                        yield return e.Current;
                    }
                    else {
                        _Running = false;
                    }
                }
            }

            FinishedHandler handler = Finished;
            if (handler != null)
                handler(_Stopped);
        }
    }

    static TaskManager _Singleton;

    public static TaskState CreateTask(IEnumerator coroutine) {
        if (_Singleton == null) {
            GameObject go = new GameObject("@TaskManager");
            DontDestroyOnLoad(go);
            _Singleton = go.AddComponent<TaskManager>();
        }
        return new TaskState(coroutine);
    }

    private void OnDestroy() {
        StopAllCoroutines();
    }
}

