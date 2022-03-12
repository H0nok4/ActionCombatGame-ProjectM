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
        //TODO:进入房间的方向
        Debug.Log("进入房间");
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
        //TODO:离开房间的方向
        ExitDir = existDir;
        BattleManager.Instance.RoomManager.ChangeRoom(existDir);
    }

    public void Update() {
        if (CharacterController.Instance.characterBase == null) {
            return;
        }

        if (CharacterController.Instance.characterBase.GameObject.transform.localPosition.x < Left) {
            //TODO:从左离开房间
            Exit(Dir.Left);
        }else if (CharacterController.Instance.characterBase.GameObject.transform.localPosition.y > Up) {
            //TODO:从上离开房间
            Exit(Dir.Up);
        }else if (CharacterController.Instance.characterBase.GameObject.transform.localPosition.x > Right) {
            //TODO:从又离开房间
            Exit(Dir.Right);
        }else if (CharacterController.Instance.characterBase.GameObject.transform.localPosition.y < Down) {
            //TODO:从下离开房间
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
        //TODO:关门
        gameobjectLayer.SetActive(true);
    }

    public virtual void EndFight() {
        RoomFight.EndFight();
        RoomFightGameObject.SetActive(false);
        //TODO:开门
        gameobjectLayer.SetActive(false);

    }
}
