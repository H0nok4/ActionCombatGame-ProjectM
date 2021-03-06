using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Map {
    public static class MapGenerator {
        public static RoomMap CreateMap(int x,int y,int count) {
            var roomMap = new RoomMap(5, 5);
            var startX = Random.Range(0, x);
            var startY = Random.Range(0, y);
            roomMap.StartX = startX;
            roomMap.StartY = startY;

            roomMap.Rooms[startX, startY].IsRoom = true;

            int index = 0;
            int curX = startX;
            int curY = startY;
            while (index < count - 1) {
                var dir = Random.Range(0, 4);//随机出方向
                switch (dir) {
                    case 0:
                        //TODO:Left
                        if (curX - 1 >= 0) {
                            //TODO:再左边尝试添加一间房间，如果重复了则不管
                            curX -= 1;
                            if (roomMap.Rooms[curX,curY].IsRoom == false) {
                                roomMap.Rooms[curX, curY].IsRoom = true;
                                index++;
                            }
                        }
                        break;
                    case 1:
                        //TODO:Right
                        if (curX + 1 < x) {
                            curX += 1;
                            if (roomMap.Rooms[curX,curY].IsRoom == false) {
                                roomMap.Rooms[curX, curY].IsRoom = true;
                                index++;
                            }
                        }
                        break;
                    case 2:
                        //TODO:Up
                        if (curY - 1 >= 0 ) {
                            curY -= 1;
                            if (roomMap.Rooms[curX,curY].IsRoom == false) {
                                roomMap.Rooms[curX, curY].IsRoom = true;
                                index++;
                            }
                        }
                        break;
                    default:
                        //TODO:Down
                        if (curY + 1 < y) {
                            curY += 1;
                            if (roomMap.Rooms[curX,curY].IsRoom == false) {
                                roomMap.Rooms[curX, curY].IsRoom = true;
                                index++;
                            }
                        }
                        break;
                }//通过不同的方向生成地图

                if (index == count - 1) {
                    roomMap.EndX = curX;
                    roomMap.EndY = curY;
                }
            }
            //从这开始，地图的雏形就有了，扫描一遍，根据每个房间的相邻房间设置房间的方向类型
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    if (roomMap.Rooms[i,j].IsRoom) {
                        var room = roomMap.Rooms[i, j];
                        var dirData = FindDirData(i, j, roomMap);
                        if (dirData.Left == true && dirData.Up == true && dirData.Right == true && dirData.Down == true) {
                            room.dirType = RoomDirType.DLRU;
                        }else if (dirData.Left == false && dirData.Up == true && dirData.Right == true && dirData.Down == true) {
                            room.dirType = RoomDirType.DRU;
                        }
                        else if (dirData.Left == true && dirData.Up == false && dirData.Right == true && dirData.Down == true) {
                            room.dirType = RoomDirType.DLR;
                        }
                        else if (dirData.Left == true && dirData.Up == true && dirData.Right == false && dirData.Down == true) {
                            room.dirType = RoomDirType.DLU;
                        }
                        else if (dirData.Left == true && dirData.Up == true && dirData.Right == true && dirData.Down == false) {
                            room.dirType = RoomDirType.LRU;
                        }
                        else if (dirData.Left == false && dirData.Up == false && dirData.Right == true && dirData.Down == true) {
                            room.dirType = RoomDirType.DR;
                        }
                        else if (dirData.Left == false && dirData.Up == true && dirData.Right == false && dirData.Down == true) {
                            room.dirType = RoomDirType.DU;
                        }
                        else if (dirData.Left == false && dirData.Up == true && dirData.Right == true && dirData.Down == false) {
                            room.dirType = RoomDirType.RU;
                        }
                        else if (dirData.Left == true && dirData.Up == false && dirData.Right == false && dirData.Down == true) {
                            room.dirType = RoomDirType.DL;
                        }
                        else if (dirData.Left == true && dirData.Up == false && dirData.Right == true && dirData.Down == false) {
                            room.dirType = RoomDirType.LR;
                        }
                        else if (dirData.Left == true && dirData.Up == true && dirData.Right == false && dirData.Down == false) {
                            room.dirType = RoomDirType.LU;
                        }
                        else if (dirData.Left == false && dirData.Up == false && dirData.Right == false && dirData.Down == true) {
                            room.dirType = RoomDirType.D;
                        }
                        else if (dirData.Left == false && dirData.Up == false && dirData.Right == true && dirData.Down == false) {
                            room.dirType = RoomDirType.R;
                        }
                        else if (dirData.Left == false && dirData.Up == true && dirData.Right == false && dirData.Down == false) {
                            room.dirType = RoomDirType.U;
                        }
                        else if (dirData.Left == true && dirData.Up == false && dirData.Right == false && dirData.Down == false) {
                            room.dirType = RoomDirType.L;
                        }
                    }

                }
            }

            //房间方向类型生成完毕，开始设置房间的战斗类型


            return roomMap;
        }

        public static RoomDirData FindDirData(int x, int y, RoomMap map) {
            RoomDirData dirData = new RoomDirData();
            if (x - 1 >= 0) {
                if (map.Rooms[x - 1, y].IsRoom) {
                    dirData.Left = true;
                }
            }

            if (x + 1 < map.MaxX) {
                if (map.Rooms[x + 1,y].IsRoom) {
                    dirData.Right = true;
                }
            }

            if (y - 1 >= 0) {
                if (map.Rooms[x,y - 1].IsRoom) {
                    dirData.Up = true;
                }
            }

            if (y + 1< map.MaxY) {
                if (map.Rooms[x,y + 1].IsRoom) {
                    dirData.Down = true;
                }
            }

            return dirData;
        }
    }

    public class Room {
        public bool IsRoom;
        public RoomDirType dirType;
        public GameObject RoomGameObject;
        public RoomBase RoomBase;
    }

    public class RoomMap {
        public Room[,] Rooms;
        public int MaxX;
        public int MaxY;
        public int StartX;
        public int StartY;
        public int EndX;
        public int EndY;
        public RoomMap(int x,int y) {
            Rooms = new Room[x, y];
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    Rooms[i, j] = new Room();
                }
            }

            MaxX = x;
            MaxY = y;
        }
    }

    public enum RoomDirType {
        L,U,R,D,
        LU,LR,DL,RU,DU,DR,
        LRU,DLU,DLR,DRU,
        DLRU
    }

    public class RoomDirData {
        public bool Left;
        public bool Right;
        public bool Up;
        public bool Down;
    }


}
