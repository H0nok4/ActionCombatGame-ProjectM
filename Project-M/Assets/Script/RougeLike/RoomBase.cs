using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBase : MonoBehaviour,RoomInterface {
    public bool left;
    public bool Up;
    public bool Right;
    public bool Down;
    public Vector2 LeftPos;
    public Vector2 UpPos;
    public Vector2 RightPos;
    public Vector2 DownPos;
    public RoomFightBase roomFight;
    public CharacterBase characterBase;
    
    public virtual void Enter(Dir enterDir) {
        //TODO:���뷿��ķ���
        Debug.Log("���뷿��");
        StartFight();
    }

    public virtual void Exit(Dir existDir) {
        //TODO:�뿪����ķ���
    }

    public void Update() {
        if (characterBase == null) {
            return;
        }

        if (characterBase.GameObject.transform.position.x < LeftPos.x) {
            //TODO:�����뿪����
            Exit(Dir.Left);
        }else if (characterBase.GameObject.transform.position.y > LeftPos.y) {
            //TODO:�����뿪����
            Exit(Dir.Up);
        }else if (characterBase.GameObject.transform.position.x > RightPos.x) {
            //TODO:�����뿪����
            Exit(Dir.Right);
        }else if (characterBase.GameObject.transform.position.y < DownPos.y) {
            //TODO:�����뿪����
            Exit(Dir.Down);
        }

        roomFight.Update();
        if (roomFight != null) {
            if (roomFight.IsClear) {
                EndFight();
                roomFight = null;
            }
        }
    }

    public virtual void StartFight() {
        roomFight.StartFight();
        //TODO:����
    }

    public virtual void EndFight() {
        roomFight.EndFight();
        //TODO:����
    }
}
