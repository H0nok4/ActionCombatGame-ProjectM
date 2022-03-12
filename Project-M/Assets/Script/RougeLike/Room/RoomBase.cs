using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomBase : MonoBehaviour,RoomInterface {
    public float Left;
    public float Up;
    public float Right;
    public float Down;
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
        if (RoomFight != null && !RoomFight.IsClear) {
            StartFight();
        }

        GetBound();
        CharacterController.Instance.characterBase.GameObject.transform.localPosition =
            new Vector3((Left + Right) / 2, (Up + Down) / 2);
    }

    public void GetBound() {
        Left = GetComponentInChildren<Tilemap>().cellBounds.xMin;
        Right = GetComponentInChildren<Tilemap>().cellBounds.xMax;
        Up = GetComponentInChildren<Tilemap>().cellBounds.yMax;
        Down = GetComponentInChildren<Tilemap>().cellBounds.yMin;
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

        if (CharacterController.Instance.characterBase.GameObject.transform.localPosition.x < Left) {
            //TODO:�����뿪����
            Exit(Dir.Left);
        }else if (CharacterController.Instance.characterBase.GameObject.transform.localPosition.y > Up) {
            //TODO:�����뿪����
            Exit(Dir.Up);
        }else if (CharacterController.Instance.characterBase.GameObject.transform.localPosition.x > Right) {
            //TODO:�����뿪����
            Exit(Dir.Right);
        }else if (CharacterController.Instance.characterBase.GameObject.transform.localPosition.y < Down) {
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
        RoomFightGameObject.SetActive(false);
        //TODO:����
        gameobjectLayer.SetActive(false);

    }
}
