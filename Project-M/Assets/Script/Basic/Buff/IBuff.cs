using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffEffect {
    void StateChange(ref int stateValue,CharacterBase character);
    void OnAttack(CharacterBase character,CharacterBase targetCharacter,ref int damage);
    void OnDefend(CharacterBase character,CharacterBase attackerCharacter,ref int damage);
    void OnSecondTick(CharacterBase characterBase);
    void OnBuffAdd();
    void OnBuffRemove();

}

