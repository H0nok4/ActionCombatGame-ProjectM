using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : CharacterStateBase
{

    public override void Enter(CharacterBase character) {
        character.MoveVec = Vector2.zero;
        character.Animator.SetBool("IsMove",false);
        character.Animator.SetBool("IsDash",false);
        character.Animator.SetBool("IsAttack",true);
    }

    public override void Run(CharacterBase character,CharacterStateMeching meching) {
        character.NormalAttack(PlayerController.Instance.GetPlayerMouseWorldPos());
        meching.ChangeState(this,BattleManager.idleState);
    }

    public override void Exit(CharacterBase character) {
        
    }
}
