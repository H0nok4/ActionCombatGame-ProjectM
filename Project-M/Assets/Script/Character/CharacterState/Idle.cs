using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : CharacterStateBase
{

    public override void Enter(CharacterBase character) {
        base.Enter(character);
        if (character.InputMoveVec != Vector2.zero) {
            character.InputMoveVec = Vector2.zero;
            character.Animator.SetBool("IsMove",false);
        }
    }

    public override void Run(CharacterBase character,CharacterStateMeching meching) {
        if (character.IsAttack == false) {
            character.UpdateMoveVec();
        }
 
        if (character.InputMoveVec == Vector2.zero) {
            character.Animator.SetBool("IsMove",false);
        }
        else {
            meching.ChangeState(this,BattleManager.moveState);
        }


        
    }

    public override void Exit(CharacterBase character) {
        //TODO:IDLE状态的Exit没啥需要处理的现在
    }
}
