using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFightBase{
    public bool IsClear;
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