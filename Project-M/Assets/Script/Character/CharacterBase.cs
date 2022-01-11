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
    public Rigidbody2D Rigbody;

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
        MoveSpeed = property.MoveSpeed;
    }

    public virtual void Burst(Vector2 inputVec) {
        throw new System.NotImplementedException();
    }

    public virtual void Dash(Vector2 inputVec) {
        //基础通用的冲刺方法，如果有特别的冲刺方式就在各自的类里实现
        Debug.Log("基础的冲刺方法");
    }

    public virtual void Move(Vector2 inputVec) {
        if (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.y) == 1) {
            Animator.SetBool("IsMove", true);
            Rigbody.velocity = inputVec * (MoveSpeed / Mathf.Sqrt(2));
        }
        else if (inputVec.x != 0 || inputVec.y != 0) {
            Animator.SetBool("IsMove", true);
            Rigbody.velocity = inputVec * MoveSpeed;
        }
        else {
            Animator.SetBool("IsMove", false);
            Rigbody.velocity = Vector2.zero;
        }

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
