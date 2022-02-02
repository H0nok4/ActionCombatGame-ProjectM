using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : CharacterStateBase
{
    long startChargeTime;
    public override void Enter(CharacterBase character) {
        character.characterState = CharacterState.Charge;
        character.IsCharge = true;
        startChargeTime = DateTime.Now.ToUniversalTime().Ticks;
        character.MoveSpeed = 2;
        //TODO:��ʼ��������
    }

    public override void Run(CharacterBase character,CharacterStateMeching meching) {
        //TODO������ʱ�䣬�����ǰ����ɿ����Ҽ�������
        character.UpdateMoveVec();
        var chargeTime = ((DateTime.Now.ToUniversalTime().Ticks - startChargeTime) / 10000000.0f);
        if (PlayerController.Instance.GetSmashHold()) {
            //���ڰ�ס�ػ���
            Debug.Log("StillCharge");
            //TODO:ͨ������ʱ�����ı䶯��

        } else {
            //�ɿ���
            Debug.Log("Smash!");
            //TODO����������ʱ���ͷ��ػ�
            Debug.Log($"Time = {chargeTime}");
            character.Smash(PlayerController.Instance.GetPlayerMouseWorldPos(),chargeTime);
            character.StateMeching.ChangeState(this,BattleManager.idleState);
            
        }
    }

    public override void Exit(CharacterBase character) {
        //��IsCharge = False
        character.IsCharge = false;
        character.MoveSpeed = 3;
    }
}
