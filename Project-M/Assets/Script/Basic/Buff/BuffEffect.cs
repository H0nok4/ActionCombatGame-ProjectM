using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : IBuffEffect {
    public Buff m_Buff;
    public virtual void StateChange(ref int stateValue,CharacterBase character)//����SrpgClassUnit��GetState�����д���
    {

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