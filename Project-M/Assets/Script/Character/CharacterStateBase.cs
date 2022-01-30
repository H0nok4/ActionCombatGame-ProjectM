using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateBase : ICharacterState {
    


    public virtual void Init(CharacterBase characterBase) {
        
        
    }
    public virtual void Enter(CharacterBase character) {
        Debug.Log("CharacterState的基础Enter方法");
    }

    public virtual void Run(CharacterBase character,CharacterStateMeching meching) {
        Debug.Log("CharacterState的基础Run方法");
    }

    public virtual void Exit(CharacterBase character) {
        Debug.Log("CharacterState的基础Exit方法");
    }
}
