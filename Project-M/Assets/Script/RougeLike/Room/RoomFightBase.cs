using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

[SerializeField]
public class RoomFightBase : MonoBehaviour{
    public bool IsClear;
    public RoomFightType Type;
    public List<EnemyBase> Enemys;

    public void Update() {
        if (IsClear) {
            //TODO:当房间清理完毕
            return;
        }

        IsClear = Check();
    }
    public virtual bool Check() {

        return false;
    }

    public virtual void StartFight() {

    }

    public virtual void EndFight() {
        //TODO:可以掉落东西之类的
    }
}

public enum RoomFightType {
    Clear,
    OpenBox,
    Destory
}