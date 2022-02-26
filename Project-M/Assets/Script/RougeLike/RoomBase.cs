using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBase : MonoBehaviour,RoomInterface
{
    public virtual void Enter(Dir enterDir) {
        //TODO:进入房间的方向
        Debug.Log("进入房间");
    }

    public virtual void Exit(Dir existDir) {
        //TODO:离开房间的方向
    }

    public virtual void StartFight() {
        throw new System.NotImplementedException();
    }

    public virtual void EndFight() {
        throw new System.NotImplementedException();
    }
}
