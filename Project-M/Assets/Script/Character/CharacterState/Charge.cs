using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : CharacterStateBase
{
    long startChargeTime;
    public override void Enter(CharacterBase character) {
        character.IsCharge = true;
        startChargeTime = DateTime.Now.ToUniversalTime().Ticks;
        character.MoveSpeed = 2;
        //TODO:开始蓄力动画
    }

    public override void Run(CharacterBase character,CharacterStateMeching meching) {
        //TODO：增加时间，如果当前玩家松开了右键，结算
        if (PlayerController.Instance.GetSmashHold()) {
            //还在按住重击键
            Debug.Log("StillCharge");
        } else {
            //松开了
            Debug.Log("Smash!");
            //TODO：根据蓄力时间释放重击
            character.StateMeching.ChangeState(this,BattleManager.idleState);
            
        }
    }

    public override void Exit(CharacterBase character) {
        //将IsCharge = False
        character.IsCharge = false;
        character.MoveSpeed = 3;
    }
}
