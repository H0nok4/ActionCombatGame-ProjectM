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
    public RoomFightBase RoomFight;
    public GameObject RoomFightGameObject;

    public GameObject gameobjectLayer;
    public Dir EnterDir;
    public Dir ExitDir;
    
    public virtual void Enter(Dir enterDir) {
        //TODO:���뷿��ķ���
        Debug.Log("���뷿��");
        EnterDir = enterDir;
        if (!RoomFight.IsClear) {
            StartFight();
        }

    }

    public virtual void Exit(Dir existDir) {
        //TODO:�뿪����ķ���
        ExitDir = existDir;
        BattleManager.Instance.RoomManager.ChangeRoom(existDir);
    }

    public void Update() {
        if (CharacterController.Instance.characterBase == null) {
            return;
        }

        if (CharacterController.Instance.characterBase.GameObject.transform.position.x < LeftPos.x) {
            //TODO:�����뿪����
            Exit(Dir.Left);
        }else if (CharacterController.Instance.characterBase.GameObject.transform.position.y > LeftPos.y) {
            //TODO:�����뿪����
            Exit(Dir.Up);
        }else if (CharacterController.Instance.characterBase.GameObject.transform.position.x > RightPos.x) {
            //TODO:�����뿪����
            Exit(Dir.Right);
        }else if (CharacterController.Instance.characterBase.GameObject.transform.position.y < DownPos.y) {
            //TODO:�����뿪����
            Exit(Dir.Down);
        }


        if (RoomFight != null) {
            if (RoomFight.IsClear) {
                EndFight();
                RoomFight = null;
            }
            else {
                RoomFight.Update();
            }
        }
    }

    public virtual void StartFight() {
        RoomFightGameObject.SetActive(true);
        RoomFight.StartFight();
        //TODO:����
        gameobjectLayer.SetActive(true);
    }

    public virtual void EndFight() {
        RoomFight.EndFight();
        //TODO:����
        gameobjectLayer.SetActive(false);
    }
}
