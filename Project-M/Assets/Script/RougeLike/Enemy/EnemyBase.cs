using System;using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

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

        StateMeching.Run();//状态机运行
        Move();//更新移动
    }

    public virtual void HalfSecond() {
        StateMeching.Thinking();//TODO：思考怎样改变状态
    }

    public virtual void OneSecond() {
        //TODO:一秒一次的事件
    }

    public void UpdateFaceDirectionByMoveVec() {
        if (MoveVec.x < 0) {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void UpdateFaceDirectionByChracter() {
        if (CharacterController.Instance.characterBase.GameObject.transform.position.x - this.transform.position.x < 0) {
            this.transform.localScale = new Vector3(-1,1,1);
        }
        else {
            transform.localScale = new Vector3(1,1,1);
        }
    }

    public void Move() {
        if (MoveVec != null && MoveVec != Vector2.zero) {
            rig.velocity = (MoveVec / MoveVec.magnitude)  * MoveSpeed;
        }
        else {
            rig.velocity = Vector2.zero;
        }
    }

    public int OnDamage(int damagePoint) {
        CurHealth -= damagePoint;
        if (CurHealth <= 0) {
            //TODO:死亡状态，可能掉落物品
            var number = Random.Range(0, 2);
            if (number == 1) {
                var coin = GameObjectPool.Instance.CreatMapObjectFromPool("Coin");
                coin.transform.position = this.gameObject.transform.position;
            }
            GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
            return 0;
        }
        
        new UnityTask(PlayAnimation(damagePoint));
        return CurHealth;
    }

    IEnumerator PlayAnimation(int DamagePoint) {
        var ondamageText = GameObjectPool.Instance.CreatUIFromPool("OnHealthChangeText");
        ondamageText.transform.parent = GameManager.Instance.MainCanvas.transform;
        var gameobjectScreenPos = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up);
        ondamageText.transform.position = Camera.main.ScreenToWorldPoint(gameobjectScreenPos);
        ondamageText.transform.localScale = Vector3.one;
        ondamageText.GetComponent<TMP_Text>().text = $"-{DamagePoint}";

        var animator = GetComponent<Animator>();
        animator.SetBool("OnDamage", true);
        yield return new WaitForSeconds(0.33f);
        animator.SetBool("OnDamage", false);
        GameObjectPool.Instance.RemoveGameObjectToPool(ondamageText);
    }


}


