using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    //��ɫ״̬
    public static readonly Attack attackState = new Attack();
    public static readonly Idle idleState = new Idle();
    public static readonly Move moveState = new Move();
    public static readonly Dash dashState = new Dash();
    public static readonly Charge chargeState = new Charge();
    public static readonly OnDamage onDamageState = new OnDamage();
    public int GoldNum = 0;
    public override void OnInitialize() {
        base.OnInitialize();
        Application.targetFrameRate = 60;
        GoldNum = 0;
    }

    public override void OnUnInitialize() {
        base.OnUnInitialize();
    }

    public void CalculateDamage(CharacterBase attacker,CharacterBase defender,int damage) {
        var realDamage = attacker.Attack + damage;

        if (attacker.Team != defender.Team) {
            defender.OnDamage(realDamage);
        }
    }

    public void CalculateDamage(CharacterBase attacker,EnemyBase defender,int damage) {
        var realDamage = attacker.Attack + damage;

        if (attacker.Team != defender.Team) {
            defender.OnDamage(realDamage);
        }
    }

    public void CalculateDamage(EnemyBase attacker,CharacterBase defender,int damage) {
        var realDamage = attacker.Attack + damage;

        if (attacker.Team != defender.Team) {
            defender.OnDamage(realDamage);
        }
    }

    public void CalculateMapObjectDamage(CharacterBase attacker,MapObjectBase mapObjectBase,int damage) {
        var realDamage = attacker.Attack + damage;
        mapObjectBase.OnDamage(realDamage);
    }
}
