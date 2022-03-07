using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Map;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoSingleton<PlayerController> {
    public bool isHoldingAttackButton;
    public bool isPressAttackButton;

    public long lastTimePressAttackButton;
    public long lastTimeFreeAttackButton;
    public long curTime;

    public bool isAttack;
    public bool isSmashed;

    private Dictionary<KeyCode, bool> keyDic = new Dictionary<KeyCode, bool>();

    private void Start() {
        var map = MapGenerator.CreateMap(5, 5, 10);
        for (int i = 0; i < map.MaxY; i++) {
            Debug.Log($"{(map.Rooms[0,i].IsRoom?"0":"+")} {(map.Rooms[1, i].IsRoom ? "0" : "+")} {(map.Rooms[2, i].IsRoom ? "0" : "+")} {(map.Rooms[3, i].IsRoom ? "0" : "+")} {(map.Rooms[4, i].IsRoom ? "0" : "+")}");
        }

        for (int i = 0; i < map.MaxY; i++) {
            Debug.Log($"{(map.Rooms[0, i].dirType)} {(map.Rooms[1, i].dirType)} {(map.Rooms[2, i].dirType)} {(map.Rooms[3, i].dirType)} {(map.Rooms[4, i].dirType)}");
        }

        Debug.Log($"开始坐标为[{map.StartX},{map.StartY}],结束坐标为[{map.EndX},{map.EndY}]");
    }

    public bool GetPlayerPressKey(KeyCode code) {
        if (Input.GetKey(code)) {
            return true;
        }

        return false;
    }

    public MousePressEvent GetFreeAttackButton() {
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            return new MousePressEvent(KeyCode.Mouse0,(lastTimePressAttackButton - ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000))/ 1000);
        }

        return null;
    }

    public bool GetPressAttackButton() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            return true;
        }

        return false;
    }

    public bool GetSmashHold() {
        if (Input.GetKey(KeyCode.Mouse1)) {
            return true;
        }

        return false;
    }

    public bool GetSmashKeyDown() {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            return true;
        }

        return false;
    }

    public bool GetSmashKeyUp() {
        if (Input.GetKeyUp(KeyCode.Mouse1)) {
            return true;
        }

        return false;
    }

    public bool GetNormalSmashInput() {
        if (isHoldingAttackButton && isSmashed == false) {
            var curTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            if (curTime - lastTimePressAttackButton > 500) {
                //��ס����������0.5��
                isSmashed = true;
                return true;
            }
        }

        return false;
    }

    

    public void UpdateAttackButton() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            lastTimePressAttackButton = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            isHoldingAttackButton = true;
            isSmashed = false;

        } else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            lastTimeFreeAttackButton = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            isHoldingAttackButton = false;
            isAttack = true;
        }


    }

    private void Update() {
        UpdateAttackButton();
    }

    private void LateUpdate() {
        isAttack = false;
    }

    /// <summary>
    /// ��������굱ǰ������λ��
    /// </summary>
    public Vector3 GetPlayerMouseWorldPos() {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePos;
    }

    /// <summary>
    /// ��Ҫ�ж�������λ�ã���ɫӦ�����������һ��
    /// </summary>
    public bool isCharacterFlip(Vector3 _characterPos) {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > _characterPos.x) {
            return true;
        }

        return false;
    }

}


public class MousePressEvent{
    KeyCode KeyCode;
    float Time;

    public MousePressEvent(KeyCode keyCode,float time) {
        KeyCode = keyCode;
        Time = time;
    }
}