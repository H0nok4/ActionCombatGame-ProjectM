using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map {
    public static class MapGenerator {
        public static RoomMap CreatMap() {

            return new RoomMap(5, 5);
        }
    }

    public class Room {
        public bool IsEmpty;
        public Room Left;
        public Room Right;
        public Room Up;
        public Room Down;
    }

    public class RoomMap {
        public Room[,] Rooms;

        public RoomMap(int x,int y) {
            Rooms = new Room[x, y];
        }
    }

}
