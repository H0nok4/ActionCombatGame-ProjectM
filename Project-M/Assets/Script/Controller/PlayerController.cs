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

    public bool GetPressAttackButton() {
        if (!isHoldingAttackButton && isSmashed == false) {
            return isAttack;
        }

        return false;
    }

    public bool GetNormalSmashInput() {
        if (isHoldingAttackButton && isSmashed == false) {
            var curTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            if (curTime - lastTimePressAttackButton > 500) {
                //��ס����������һ��
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
