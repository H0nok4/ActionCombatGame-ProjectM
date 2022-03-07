using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RoomInterface {
    void Enter(Dir enterDir);
    void Exit(Dir existDir);
    void StartFight();
    void EndFight();
}

public enum Dir {
    Left,
    Right,
    Up,
    Down
}