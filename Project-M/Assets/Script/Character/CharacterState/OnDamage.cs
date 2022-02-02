using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnDamage : CharacterStateBase
{
    long enterTime;
    public override void Enter(CharacterBase character) {
        character.characterState = CharacterState.OnDamage;
        //TODO:播放受击动画，0.1秒后转为Idle状态,可以通过闪避打断后摇
        enterTime = DateTime.Now.ToUniversalTime().Ticks;
    }

    public override void Run(CharacterBase character,CharacterStateMeching meching) {
        var curTime = (DateTime.Now.ToUniversalTime().Ticks - enterTime) / 10000000.0f;
        if (curTime > 0.1f) {
            character.StateMeching.ChangeState(this,BattleManager.idleState);
        }
    }

    public override void Exit(CharacterBase character) {
        
    }
}
