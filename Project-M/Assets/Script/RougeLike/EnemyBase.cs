using System;using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EnemyBase : MonoBehaviour,IDamageable {
    public EnemyStateMeching StateMeching;
    public EnemyProt Prot;

    public int CurHealth;
    public int Attack;
    public int Defense;
    public int MoveSpeed;
    public int MaxHealth;

    public Team Team;
    public float Timer = 0;
    public int Tick = 0;

    public Vector2 MoveVec;
    public Rigidbody2D rig;

    public virtual void Init(EnemyProt property,Team team) {
        Team = team;
        InitWithEnemyProperty(property);
    }
    public void InitWithEnemyProperty(EnemyProt property) {
        MaxHealth = property.MaxHealth;
        MoveSpeed = property.MoveSpeed;
        Defense = property.Defense;
        Attack = property.Attack;
        CurHealth = MaxHealth;
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

        StateMeching.Run();//状态机运行
        Move();//更新移动
    }

    public virtual void HalfSecond() {
        StateMeching.Thinking();//TODO：思考怎样改变状态
    }

    public virtual void OneSecond() {

    }

    public void Move() {
        if (MoveVec != null) {
            rig.velocity = MoveVec * MoveSpeed * Time.deltaTime;
        }
    }

    public int OnDamage(int damagePoint) {
        CurHealth -= damagePoint;
        if (CurHealth <= 0) {
            //TODO:死亡状态，可能掉落物品
        }


        return CurHealth;
    }
}

[CreateAssetMenu(fileName = "New EnemyProt",menuName = "Create EnemyProt")]
public class EnemyProt : ScriptableObject {
    public int MaxHealth;
    public int Attack;
    public int Defense;
    public int MoveSpeed;
}

