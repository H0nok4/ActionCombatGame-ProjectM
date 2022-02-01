using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public void Init(CharacterProperty property,Team team);

    public void Dash(Vector2 inputVec);

    public void NormalAttack(Vector2 inputVec);

    public void Smash(Vector2 inputVec,float chargeTime);

    public void Move(Vector2 inputVec);

    public void Skill(Vector2 inputVec);

    public void Burst(Vector2 inputVec);

    public void StartCharge();

    public void EndCharge();
}
