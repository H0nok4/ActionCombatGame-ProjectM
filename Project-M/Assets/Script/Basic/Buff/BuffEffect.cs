using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : IBuffEffect {
    public Buff m_Buff;
    public virtual void StateChange_Add(ref int stateValue,StateType stateType)//����SrpgClassUnit��GetState�����д���
    {

    }

    public virtual void StateChange_Plus(ref int stateValue, StateType stateType) {

    }

    public virtual void OnAttack(CharacterBase character,CharacterBase targetCharacter,ref int damage)//����SrpgClassUnit�ı����������д���
    {

    }

    public virtual void OnDefend(CharacterBase character,CharacterBase attackerCharacter,ref int damage)//����SrpgClassUnit�ı����������д���
    {

    }

    public virtual void OnSecondTick(CharacterBase characterBase) {
    
    }

    public virtual void OnBuffAdd() {

    }

    public virtual void OnBuffRemove() {

    }


}