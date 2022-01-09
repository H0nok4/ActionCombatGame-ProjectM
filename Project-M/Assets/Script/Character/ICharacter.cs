using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public void Init(CharacterProperty property);

    public void Dash();

    public void NormalAttack();

    public void Smash();

    public void Move();

    public void Skill();

    public void Burst();

    public int OnDamage();

}
