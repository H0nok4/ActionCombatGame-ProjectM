using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager {
    public static string[] RoomSize = new string[] {"4X4", "6X4", "6X8", "8X5", "8X7", "10X5"};
    public RoomMap Map;
    public int CurX;
    public int CurY;
    public Room CurRoom;
    public void Init() {
        Map = MapGenerator.CreateMap(5, 5, 10);
        CurX = Map.StartX;
        CurY = Map.StartY;
        for (int i = 0; i < Map.MaxX; i++) {
            for (int j = 0; j < Map.MaxY; j++) {
                if (Map.Rooms[i,j].IsRoom) {
                    //TODO:生成一个随机的房间实例
                    var roomSize = GetRamdonRoomSize();
                    Map.Rooms[i, j].RoomGameObject =
                        GameObjectPool.Instance.CreatRoomFromPool( roomSize+ "Room_" +
                                                                  Map.Rooms[i, j].dirType.ToString());
                    Map.Rooms[i, j].RoomBase = Map.Rooms[i, j].RoomGameObject.GetComponent<RoomBase>();
                    Map.Rooms[i, j].RoomGameObject.SetActive(false);
                    Map.Rooms[i, j].RoomBase.RoomFightGameObject =
                        GameObjectPool.Instance.CreatRoomFightFromPool($"{roomSize}_ClearRoom_1");//TODO:需要随机战斗类型
                    Map.Rooms[i, j].RoomBase.RoomFight =
                        Map.Rooms[i, j].RoomBase.RoomFightGameObject.GetComponent<RoomFightBase>();
                    Map.Rooms[i,j].RoomBase.RoomFightGameObject.SetActive(false);
                    Map.Rooms[i,j].RoomGameObject.SetActive(false);
                }
            }
        }
    }

    public void EnterStartRoom() {
        CurRoom = Map.Rooms[CurX, CurY];
        Map.Rooms[CurX, CurY].RoomGameObject.SetActive(true);
        CurRoom.RoomBase.RoomFight.IsClear = true;
        Map.Rooms[CurX, CurY].RoomGameObject.transform.position = new Vector3(0, 0);
        CharacterController.Instance.characterBase.GameObject.transform.position = new Vector3(0, 0);
        Map.Rooms[CurX, CurY].RoomBase.gameobjectLayer.SetActive(false);
    }

    public void ChangeRoom(Dir toDir) {
        CurRoom.RoomGameObject.SetActive(false);
        switch (toDir) {
            case Dir.Down:
                //TODO:向下走
                if (CurY + 1 < Map.MaxY) {
                    CurY++;
                }
                break;
            case Dir.Left:
                //TODO:向左走
                if (CurX - 1 >= 0) {
                    CurX--;
                }
                break;
            case Dir.Right:
                //TODO:向右走
                if (CurX + 1 < Map.MaxX) {
                    CurX++;
                }
                break;
            case Dir.Up:
                //TODO:向上走
                if (CurY - 1 >= 0) {
                    CurY--;
                }
                break;
        }
        ChangeRoom(Map.Rooms[CurX, CurY], toDir);
    }

    public void ChangeRoom(Room nextRoom,Dir enterDir) {
        nextRoom.RoomGameObject.SetActive(true);
        CurRoom = nextRoom;
        switch (enterDir) {
            case Dir.Down:
                //TODO;从上面进入
                nextRoom.RoomBase.Enter(Dir.Up);
                break;
            case Dir.Up:
                nextRoom.RoomBase.Enter(Dir.Down);
                break;
            case Dir.Left:
                nextRoom.RoomBase.Enter(Dir.Right);
                break;
            case Dir.Right:
                nextRoom.RoomBase.Enter(Dir.Left);
                break;
        }

    }

    public string GetRamdonRoomSize() {
        return RoomSize[Random.Range(0, RoomSize.Length)];
    }
}

