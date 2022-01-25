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

    public override void Run(CharacterBase character) {
        character.UpdateMoveVec();
        character.Move(character.MoveVec);
    }



    public override void Exit(CharacterBase character) {
        character.Animator.SetBool("IsMove",false);
    }
}
