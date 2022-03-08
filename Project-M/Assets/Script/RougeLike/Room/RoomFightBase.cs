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
            //TODO:�������������
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
        //TODO:���Ե��䶫��֮���
    }
}

public enum RoomFightType {
    Clear,
    OpenBox,
    Destory
}