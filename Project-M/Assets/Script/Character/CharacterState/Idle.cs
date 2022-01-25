using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : CharacterStateBase
{

    public override void Enter(CharacterBase character) {
        base.Enter(character);
        if (character.MoveVec != Vector2.zero) {
            character.MoveVec = Vector2.zero;
            character.Animator.SetBool("IsMove",false);
        }
    }

    public override void Run(CharacterBase character) {
        character.MoveVec = Vector2.zero;
        character.Animator.SetBool("IsMove",false);
    }

    public override void Exit(CharacterBase character) {
        //TODO:IDLE״̬��Exitûɶ��Ҫ���������
    }
}
