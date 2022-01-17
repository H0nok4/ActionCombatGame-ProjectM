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
        //����ͨ�õĳ�̷�����������ر�ĳ�̷�ʽ���ڸ��Ե�����ʵ��
        Debug.Log("�����ĳ�̷���");
        //�Ҽ���̣������ɫ���ٶȵĻ��������ٶȷ�����һ�ξ��룬���û���ٶȣ�������귽����һ�ξ���
        //��̹�����Ӧ�����޵�֡ 
        //��ʱ���ڳ����CD
        //�����������
        //��̺��������ס��̼������뱼��״̬
        if (MoveState == MoveState.Move ||MoveState == MoveState.Idle) {
            if (Rigbody.velocity == Vector2.zero) {
                //û���ٶȣ�������귽����һ�ξ���
                MoveState = MoveState.Dash;
                new UnityTask(StartDash(inputVec));
            } else {
                //��ǰ���ٶȣ������ٶȷ�����һ�ξ���
                MoveState = MoveState.Dash;
                new UnityTask(StartDash(new Vector2(GameObject.transform.position.x,GameObject.transform.position.y) + Rigbody.velocity));
            }
        }

    }

    IEnumerator StartDash(Vector2 inputVector) {
        //��ʼ���
        Animator.SetBool("IsDash",true);
        Rigbody.velocity = (inputVector - new Vector2(GameObject.transform.position.x,GameObject.transform.position.y)).normalized * 6;
        Invincible = true;
        yield return new WaitForSeconds(0.1f);
        //�����޵�֡�ж�
        Invincible = false;
        yield return new WaitForSeconds(0.15f);
        //���ֹͣ
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
                //�����ָ��ķ����ǰ������ļнǴ���90�㣬˵���ƶ���Ŀ�ӷ����Ƿ���ģ������ƶ��ٶ�
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
