using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : CharacterStateBase
{
    public override void Init(CharacterBase characterBase) {

    }
    public override void Enter(CharacterBase character) {
        if (character.Animator.GetBool("IsMove") == false) {
            character.Animator.SetBool("IsMove",true);
        }
    }

    public override void Run(CharacterBase character,CharacterStateMeching meching) {
        character.UpdateMoveVec();
        if (character.InputMoveVec == Vector2.zero) {
            meching.ChangeState(this,BattleManager.idleState);
        }
        else {
            character.Animator.SetBool("IsMove",true);
        }


    }



    public override void Exit(CharacterBase character) {
        character.Animator.SetBool("IsMove",false);
    }
}
