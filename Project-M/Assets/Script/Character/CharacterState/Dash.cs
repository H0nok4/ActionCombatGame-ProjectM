using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : CharacterStateBase {

    public override void Enter(CharacterBase character) {
        character.characterState = CharacterState.Dash;
        if (character.CurEnergy < 20) {
            character.StateMeching.ChangeState(character.StateMeching.curState,character.StateMeching.preState);
        }
    }

    public override void Run(CharacterBase character,CharacterStateMeching meching) {
        if (character.CurEnergy >= 20 && character.IsDash == false) {
            character.Dash(PlayerController.Instance.GetPlayerMouseWorldPos());
        } else if (character.IsDash) {
            //µÈ´ý
        } else {
            character.StateMeching.ChangeState(character.StateMeching.curState,character.StateMeching.preState);
        }


    }



    public override void Exit(CharacterBase character) {
        character.Animator.SetBool("IsDash",false);
    }
}