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
        //TODO:��ʼ��������
    }

    public override void Run(CharacterBase character,CharacterStateMeching meching) {
        //TODO������ʱ�䣬�����ǰ����ɿ����Ҽ�������
        if (PlayerController.Instance.GetSmashHold()) {
            //���ڰ�ס�ػ���
            Debug.Log("StillCharge");
        } else {
            //�ɿ���
            Debug.Log("Smash!");
            //TODO����������ʱ���ͷ��ػ�
            character.StateMeching.ChangeState(this,BattleManager.idleState);
            
        }
    }

    public override void Exit(CharacterBase character) {
        //��IsCharge = False
        character.IsCharge = false;
        character.MoveSpeed = 3;
    }
}
