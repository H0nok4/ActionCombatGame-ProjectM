using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBase : MonoBehaviour,RoomInterface
{
    public virtual void Enter(Dir enterDir) {
        //TODO:���뷿��ķ���
        Debug.Log("���뷿��");
    }

    public virtual void Exit(Dir existDir) {
        //TODO:�뿪����ķ���
    }

    public virtual void StartFight() {
        throw new System.NotImplementedException();
    }

    public virtual void EndFight() {
        throw new System.NotImplementedException();
    }
}
