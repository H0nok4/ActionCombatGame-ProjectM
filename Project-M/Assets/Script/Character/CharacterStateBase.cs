using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateBase : ICharacterState {
    


    public virtual void Init(CharacterBase characterBase) {
        
        
    }
    public virtual void Enter(CharacterBase character) {
        Debug.Log("CharacterState�Ļ���Enter����");
    }

    public virtual void Run(CharacterBase character,CharacterStateMeching meching) {
        Debug.Log("CharacterState�Ļ���Run����");
    }

    public virtual void Exit(CharacterBase character) {
        Debug.Log("CharacterState�Ļ���Exit����");
    }
}
