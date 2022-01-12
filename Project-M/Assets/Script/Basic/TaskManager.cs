using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using System;

public class UnityTask : INotifyCompletion {
    /// <summary>
    /// 当且仅当协程在运行时返回true(协程暂停，已然认为是处于Runnig状态)
    /// </summary>
    public bool Running => _Task.Running;

    public bool IsCompleted => !Running;

    public void GetResult() {
    }

    /// <summary>
    /// 当且仅当协程处于暂停状态时，返回True
    /// </summary>
    public bool Paused => _Task.Paused;

    /// 当且仅当协程是被手动调用Stop接口停止是，manaul指为true
    public delegate void FinishedHandler(bool manual);

    /// <summary>
    /// 协程结束事件，协程执行完毕后触发该事件
    /// </summary>
    public event FinishedHandler Finished;


    TaskManager.TaskState _Task;
    Action _Continuation;

    /// <summary>
    /// 为指定的协程创建一个Task对象，当autoStart为true（默认为true）的时候，
    /// task在构造过程中自动开始
    /// </summary>
    /// <param name="c"></param>
    /// <param name="autoStart">自动开始</param>
    public UnityTask(IEnumerator c, bool autoStart = true) {
        _Task = TaskManager.CreateTask(c);
        _Task.Finished += TaskFinished;
        if (autoStart)
            Start();
    }

    /// <summary>
    /// 开始执行协程
    /// </summary>
    public void Start() {
        _Task.Start();
    }

    /// <summary>
    /// 在协程的下一次yield时中止协程
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

