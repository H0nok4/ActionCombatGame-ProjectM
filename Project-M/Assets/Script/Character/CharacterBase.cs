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

    public GameObject GameObject;
    public SpriteRenderer Sprite;
    public Weapon Weapon;
    public Animator Animator;

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

    public virtual void Burst(Vector2 inputVec) {
        throw new System.NotImplementedException();
    }

    public virtual void Dash(Vector2 inputVec) {
        throw new System.NotImplementedException();
    }

    public virtual void Move(Vector2 inputVec) {
        throw new System.NotImplementedException();
    }

    public virtual void NormalAttack(Vector2 inputVec) {
        throw new System.NotImplementedException();
    }

    public virtual int OnDamage(int damage) {
        throw new System.NotImplementedException();
    }

    public virtual void Skill(Vector2 inputVec) {
        throw new System.NotImplementedException();
    }

    public virtual void Smash(Vector2 inputVec) {
        throw new System.NotImplementedException();
    }
}
