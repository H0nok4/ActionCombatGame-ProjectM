using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : CharacterStateBase
{
    long startChargeTime;
    public override void Enter(CharacterBase character) {
        //TODO������ҵ�IsCharge = True;
        //TODO����¼�µ�ǰʱ��ΪstartChargeTime
    }

    public override void Run(CharacterBase character,CharacterStateMeching meching) {
        //TODO������ʱ�䣬�����ǰ����ɿ����Ҽ�������
    }

    public override void Exit(CharacterBase character) {
        //��IsCharge = False
    }
}
