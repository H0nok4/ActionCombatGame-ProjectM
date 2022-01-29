using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    //½ÇÉ«×´Ì¬
    public static readonly Attack attackState = new Attack();
    public static readonly Idle idleState = new Idle();
    public static readonly Move moveState = new Move();
    public static readonly Dash dashState = new Dash();

    public override void OnInitialize() {
        base.OnInitialize();
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

    public void CalculateMapObjectDamage(CharacterBase attacker,IMapObjectBase mapObjectBase,int damage) {
        var realDamage = attacker.Attack + damage;
        mapObjectBase.OnDamage(realDamage);
    }
}
