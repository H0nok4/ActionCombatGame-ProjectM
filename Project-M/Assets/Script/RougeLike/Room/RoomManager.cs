using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public class RoomManager{
    public RoomMap Map;
    public void Init() {
        Map = MapGenerator.CreateMap(5, 5, 10);
    }
}
