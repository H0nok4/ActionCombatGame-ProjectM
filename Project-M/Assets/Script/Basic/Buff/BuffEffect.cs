using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : IBuffEffect {
    public Buff m_Buff;
    public virtual void StateChange_Add(ref int stateValue,StateType stateType)//√在SrpgClassUnit的GetState方法中触发
    {

    }

    public virtual void StateChange_Plus(ref int stateValue, StateType stateType) {

    }

    public virtual void OnAttack(CharacterBase character,CharacterBase targetCharacter,ref int damage)//√在SrpgClassUnit的被攻击方法中触发
    {

    }

    public virtual void OnDefend(CharacterBase character,CharacterBase attackerCharacter,ref int damage)//√在SrpgClassUnit的被攻击方法中触发
    {

    }

    public virtual void OnSecondTick(CharacterBase characterBase) {
    
    }

    public virtual void OnBuffAdd() {

    }

    public virtual void OnBuffRemove() {

    }


}