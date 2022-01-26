using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    //½ÇÉ«×´Ì¬
    public static readonly Attack attackState = new Attack();
    public static readonly Idle idleState = new Idle();
    public static readonly Move moveState = new Move();

    public override void OnInitialize() {
        base.OnInitialize();
    }

    public override void OnUnInitialize() {
        base.OnUnInitialize();
    }
}
