using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState {
    void Init(CharacterBase character);
    void Enter(CharacterBase character);
    void Run(CharacterBase character,CharacterStateMeching meching);
    void Exit(CharacterBase character);
}
