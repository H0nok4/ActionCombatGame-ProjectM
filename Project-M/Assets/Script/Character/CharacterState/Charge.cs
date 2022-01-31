using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : CharacterStateBase
{
    long startChargeTime;
    public override void Enter(CharacterBase character) {
        //TODO：将玩家的IsCharge = True;
        //TODO：记录下当前时间为startChargeTime
    }

    public override void Run(CharacterBase character,CharacterStateMeching meching) {
        //TODO：增加时间，如果当前玩家松开了右键，结算
    }

    public override void Exit(CharacterBase character) {
        //将IsCharge = False
    }
}
