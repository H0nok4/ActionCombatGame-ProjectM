using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffEffect {
    void StateChange_Add(ref int stateValue,StateType stateType);
    void StateChange_Plus(ref int stateValue, StateType stateType);
    void OnAttack(CharacterBase character,CharacterBase targetCharacter,ref int damage);
    void OnDefend(CharacterBase character,CharacterBase attackerCharacter,ref int damage);
    void OnSecondTick(CharacterBase characterBase);
    void OnBuffAdd();
    void OnBuffRemove();

}

