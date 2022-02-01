using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState{
    Move,
    Attack,
    Idle
}

public enum Team {
    red,
    blue,
    wild
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
    public Team Team;

    public bool Invincible;//�޵�

    public long LastTimeUseEnergy;

    //����״̬���

    public Vector2 MoveVec;
    public Vector2 InputMoveVec;
    public bool IsAttack;
    public bool IsDash;
    public bool IsCharge;

    public GameObject GameObject;
    public SpriteRenderer Sprite;
    public Weapon Weapon;
    public Animator Animator;
    public Rigidbody2D Rigbody;
    public CharacterStateMeching StateMeching;

    public virtual void Init(CharacterProperty property,Team team) {
        InitWithCharacterProperty(property,team);
    }
    public void InitWithCharacterProperty(CharacterProperty property,Team team) {
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

        Team = team;
    }

    public virtual void Burst(Vector2 inputVec) {
        throw new System.NotImplementedException();
    }

    public virtual void Dash(Vector2 inputVec) {
        //����ͨ�õĳ�̷�����������ر�ĳ�̷�ʽ���ڸ��Ե�����ʵ��

        //�Ҽ���̣������ɫ���ٶȵĻ��������ٶȷ�����һ�ξ��룬���û���ٶȣ�������귽����һ�ξ���
        //��̹�����Ӧ�����޵�֡ 
        //��ʱ���ڳ����CD
        //�����������
        //��̺��������ס��̼������뱼��״̬

        //Temp:�����Ҫ��������
        Debug.Log(CurEnergy);
        if (IsDash == false && UseEnergy(40)) {
            Debug.Log("�����ĳ�̷���");
            IsAttack = false;
            IsDash = true;
            if (Rigbody.velocity == Vector2.zero && InputMoveVec == Vector2.zero) {
                //û���ٶȣ�������귽����һ�ξ���
                new UnityTask(StartDash(inputVec));
            } else {
                //��ǰ���ٶȣ������ٶȷ�����һ�ξ���
                new UnityTask(StartDash(new Vector2(GameObject.transform.position.x,GameObject.transform.position.y) + InputMoveVec));
            }
        } else {
            StateMeching.ChangeState(StateMeching.curState,StateMeching.preState);
        }
        
    }

    public virtual void Update() {
        StateMeching.Run();
        UpdateInputVec();
    }



    public void UpdateInputVec() {
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

        InputMoveVec = new Vector2(MoveX, MoveY);
    }

    public virtual void FixUpdate() {
        //���½�ɫ������״̬�������ƶ���
        UpdatePosition();
    }

    public void UpdatePosition() {
        //TODO:��������������½�ɫ��״̬
        //ƽ������Input����Ҫ���µ�ʱ����ܸ���λ��
        if (IsAttack == false) {
            Move(MoveVec);
        }


    }

    public void UpdateMoveVec() {
        MoveVec = InputMoveVec;
    }

    IEnumerator StartDash(Vector2 inputVector) {
        //��ʼ���
        MoveVec = (inputVector - new Vector2(GameObject.transform.position.x,GameObject.transform.position.y)).normalized * 3;
        Invincible = true;
        yield return new WaitForSeconds(0.1f);
        //�����޵�֡�ж�
        Invincible = false;
        yield return new WaitForSeconds(0.15f);
        //���ֹͣ
        MoveVec = Vector2.zero;
        IsDash = false;
        StateMeching.ChangeState(BattleManager.dashState,BattleManager.idleState);

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

    public void StartAttack() {
        StateMeching.ChangeState(StateMeching.curState,BattleManager.attackState);
    }

    public virtual void NormalAttack(Vector2 inputVec) {
        Debug.Log("�����Ĺ�������");
        //TODO:��������ȴ
        IsAttack = true;
    }


    public virtual bool UseEnergy(int energy) {
        var curTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        //������������
        if (CurEnergy >= energy) {
            CurEnergy -= energy;
            LastTimeUseEnergy = curTime;
            return true;
        }

        HPBarController.Instance.UpdateEnergy(CurEnergy);
        return false;
    }

    public virtual void RecoverEnergy() {
        if (CurEnergy < MaxEnergy) {
            var curTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            if (curTime - LastTimeUseEnergy > 1 && CurEnergy < MaxEnergy) {
                //TODO:�ָ�����
                CurEnergy += 10 * Time.fixedDeltaTime;
            }

            if (CurEnergy > MaxEnergy) {
                CurEnergy = MaxEnergy;
            }

            HPBarController.Instance.UpdateEnergy(CurEnergy);
        }
    }

    public virtual void Skill(Vector2 inputVec) {
        throw new System.NotImplementedException();
    }

    public virtual void Smash(Vector2 inputVec,float chargeTime) {
        Debug.Log("�������ػ�����");
    }

    public virtual int OnDamage(int damagePoint) {
        Debug.Log("�������ܻ�����");
        CurHealth -= damagePoint;

        if (CurHealth <= 0) {
            Debug.Log("��ɫ����");
        }

        HPBarController.Instance.UpdateHP(CurHealth);
        return CurHealth;
    }

    public virtual void StartCharge() {
        Debug.Log("StartCharge");
    }

    public virtual void EndCharge() {
        Debug.Log("������EndCharge����");
    }

}
