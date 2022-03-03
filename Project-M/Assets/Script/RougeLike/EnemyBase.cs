using System;using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EnemyBase : MonoBehaviour,IDamageable {
    public EnemyStateMeching StateMeching;
    public EnemyProperty Prot;

    public int CurHealth;
    public int Attack;
    public int MoveSpeed;
    public int MaxHealth;

    public Team Team;
    public float Timer = 0;
    public int Tick = 0;

    public Vector2 MoveVec;
    public Rigidbody2D rig;

    private void Start() {
        Init(Prot,Team.Enemy);
    }

    public virtual void Init(EnemyProperty property,Team team) {
        Team = team;
        InitWithEnemyProperty(property);
        StateMeching = new EnemyStateMeching();
        StateMeching.Init(this);
    }
    public void InitWithEnemyProperty(EnemyProperty property) {
        MaxHealth = property.Health;
        MoveSpeed = property.MoveSpeed;
        Attack = property.Attack;
        CurHealth = MaxHealth;

        rig = GetComponent<Rigidbody2D>();
    }

    public void Update() {
        
        if (Timer + 0.5 < Time.time) {
            Timer = Time.time;
            Tick++;
            HalfSecond();
        }

        if (Tick >= 2) {
            Tick -= 2;
            OneSecond();
        }

        StateMeching.Run();//״̬������
        Move();//�����ƶ�
    }

    public virtual void HalfSecond() {
        StateMeching.Thinking();//TODO��˼�������ı�״̬
    }

    public virtual void OneSecond() {
        //TODO:һ��һ�ε��¼�
    }

    public void Move() {
        if (MoveVec != null) {
            rig.velocity = (MoveVec / MoveVec.magnitude)  * MoveSpeed;
        }
    }

    public int OnDamage(int damagePoint) {
        CurHealth -= damagePoint;
        if (CurHealth <= 0) {
            //TODO:����״̬�����ܵ�����Ʒ
        }


        return CurHealth;
    }
}


