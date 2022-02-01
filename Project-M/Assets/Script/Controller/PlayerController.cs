using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
                //按住攻击键超过0.5秒
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
    /// 获得玩家鼠标当前的世界位置
    /// </summary>
    public Vector3 GetPlayerMouseWorldPos() {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePos;
    }

    /// <summary>
    /// 需要判断玩家鼠标位置，角色应该面向鼠标那一侧
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