using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState {
    Idle,
    Move,
    Dash
}

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

    public MoveState MoveState;
    public bool Invincible;

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
        //右键冲刺，如果角色有速度的话，朝着速度方向冲刺一段距离，如果没有速度，朝着鼠标方向冲刺一段距离
        //冲刺过程中应该有无敌帧 
        //短时间内冲刺有CD
        //冲刺消耗体力
        //冲刺后如果还按住冲刺键，进入奔跑状态
        if (MoveState == MoveState.Move ||MoveState == MoveState.Idle) {
            if (Rigbody.velocity == Vector2.zero) {
                //没有速度，朝着鼠标方向冲刺一段距离
                MoveState = MoveState.Dash;
                new UnityTask(StartDash(inputVec));
            } else {
                //当前有速度，朝着速度方向冲刺一段距离
                MoveState = MoveState.Dash;
                new UnityTask(StartDash(new Vector2(GameObject.transform.position.x,GameObject.transform.position.y) + Rigbody.velocity));
            }
        }

    }

    IEnumerator StartDash(Vector2 inputVector) {
        //开始冲刺
        Animator.SetBool("IsDash",true);
        Rigbody.velocity = (inputVector - new Vector2(GameObject.transform.position.x,GameObject.transform.position.y)).normalized * 6;
        Invincible = true;
        yield return new WaitForSeconds(0.1f);
        //闪避无敌帧判定
        Invincible = false;
        yield return new WaitForSeconds(0.15f);
        //冲刺停止
        Rigbody.velocity = Vector2.zero;
        Animator.SetBool("IsDash",false);
        MoveState = MoveState.Move;

    }

    public virtual void Move(Vector2 inputVec) {
        if (MoveState == MoveState.Move) {
            if (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.y) == 1) {
                Animator.SetBool("IsMove",true);
                Rigbody.velocity = inputVec * (MoveSpeed / Mathf.Sqrt(2));
            } else if (inputVec.x != 0 || inputVec.y != 0) {
                Animator.SetBool("IsMove",true);
                Rigbody.velocity = inputVec * MoveSpeed;
            } else {
                Animator.SetBool("IsMove",false);
                Rigbody.velocity = Vector2.zero;
            }

            if (Mathf.Abs(Vector2.Angle(Rigbody.velocity,(PlayerController.Instance.GetPlayerMouseWorldPos() - GameObject.transform.position))) > 90) {
                //当鼠标指向的方向和前进方向的夹角大于90°，说明移动和目视方向是反向的，减缓移动速度
                Rigbody.velocity /= 1.5f;
            }
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
