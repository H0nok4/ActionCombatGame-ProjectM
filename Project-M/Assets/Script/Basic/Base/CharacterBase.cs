using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState{
    Move,
    Attack,
    Idle
}

public class CharacterBase : ICharacter,IDamageable {
    public int MaxHealth;
    public int CurHealth;
    public int Attack;
    public float AttackRange;
    public int MaxEnergy;
    public float CurEnergy;
    public int MaxBurstEnergy;
    public int CurBurstEnergy;
    public int MoveSpeed;
    public string CharacterName;

    public bool Invincible;

    public long LastTimeUseEnergy;

    //����״̬���
    public Vector2 MoveVec;

    public GameObject GameObject;
    public SpriteRenderer Sprite;
    public Weapon Weapon;
    public Animator Animator;
    public Rigidbody2D Rigbody;
    public CharacterStateMeching StateMeching;

    public virtual void Init(CharacterProperty property) {
        InitWithCharacterProperty(property);
    }
    public void InitWithCharacterProperty(CharacterProperty property) {
        MaxHealth = property.Health;
        CurHealth = MaxHealth;
        Attack = property.Attack;
        AttackRange = property.AttackRange;
        MaxEnergy = property.Energy;
        CurEnergy = MaxEnergy;
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
        
            //Temp:�����Ҫ��������
        Debug.Log($"��ǰ����Ϊ��{CurEnergy}");
        if (UseEnergy(20)) {
            if (Rigbody.velocity == Vector2.zero && MoveVec == Vector2.zero) {
                //û���ٶȣ�������귽����һ�ξ���
                new UnityTask(StartDash(inputVec));
            } else {
                //��ǰ���ٶȣ������ٶȷ�����һ�ξ���
                new UnityTask(StartDash(new Vector2(GameObject.transform.position.x,GameObject.transform.position.y) + MoveVec));
            }
        }
        
    }

    public virtual void Update() {
        StateMeching.Run();
    }



    public void UpdateMoveVec() {
        int MoveX = 0;
        int MoveY = 0;

        if (Input.GetKey(KeyCode.A)) {
            MoveX -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            MoveX += 1;
        }
        if (Input.GetKey(KeyCode.W)) {
            MoveY += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            MoveY -= 1;
        }

        MoveVec = new Vector2(MoveX, MoveY);
    }

    public virtual void FixUpdate() {
        //���½�ɫ������״̬�������ƶ���
        UpdatePosition();
    }

    public void UpdatePosition() {
        //TODO:��������������½�ɫ��״̬
        Move(MoveVec);
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

    }

    public virtual void Move(Vector2 inputVec) {
        
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

    public virtual void NormalAttack(Vector2 inputVec) {
        Debug.Log("�����Ĺ�������");
        //TODO:��������ȴ
        StateMeching.ChangeState(StateMeching.curState,BattleManager.attackState);
    }

    public virtual bool UseEnergy(int energy) {
        var curTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        //������������
        if (CurEnergy >= energy) {
            CurEnergy -= energy;
            LastTimeUseEnergy = curTime;
            return true;
        }

        return false;
    }

    public virtual void RecoverEnergy() {
        var curTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        if (curTime - LastTimeUseEnergy > 1 && CurEnergy < MaxEnergy) {
            //TODO:�ָ�����
            CurEnergy += 10 * Time.fixedDeltaTime;
        }

        if (CurEnergy > MaxEnergy) {
            CurEnergy = MaxEnergy;
        }
    }

    public virtual void Skill(Vector2 inputVec) {
        throw new System.NotImplementedException();
    }

    public virtual void Smash(Vector2 inputVec) {
        Debug.Log("�������ػ�����");
    }

    public virtual int OnDamage(int damagePoint) {
        Debug.Log("�������ܻ�����");
        CurHealth -= damagePoint;

        if (CurHealth <= 0) {
            Debug.Log("��ɫ����");
        }

        return CurHealth;
    }
}
