using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : ICharacter {
    public int MaxHealth;
    public int CurHealth;
    public int Attack;
    public float AttackRange;
    public int Energy;
    public int MaxBurstEnergy;
    public int CurBurstEnergy;
    public int MoveSpeed;
    public string CharacterName;

    public GameObject CharacterGameObject;
    public GameObject CharacterSprite;
    public GameObject CharacterWeaponObject;
    public Animator CharacterAnimator;

    public virtual void Init(CharacterProperty property) {
        InitWithCharacterProperty(property);
    }
    public void InitWithCharacterProperty(CharacterProperty property) {
        MaxHealth = property.Health;
        CurHealth = MaxHealth;
        Attack = property.Attack;
        AttackRange = property.AttackRange;
        Energy = property.Energy;
        MaxBurstEnergy = property.BurstEnergy;
        CurBurstEnergy = 0;
        CharacterName = property.CharacterName;
    }

    public virtual void Burst() {
        throw new System.NotImplementedException();
    }

    public virtual void Dash() {
        throw new System.NotImplementedException();
    }

    public virtual void Move() {
        throw new System.NotImplementedException();
    }

    public virtual void NormalAttack() {
        throw new System.NotImplementedException();
    }

    public virtual int OnDamage() {
        throw new System.NotImplementedException();
    }

    public virtual void Skill() {
        throw new System.NotImplementedException();
    }

    public virtual void Smash() {
        throw new System.NotImplementedException();
    }
}
