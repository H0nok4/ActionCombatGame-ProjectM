using System.Collections;
using System.Collections.Generic;using System.Linq.Expressions;
using UnityEngine;

public class EnemyStateMeching {
    public EnemyStateBase idle = new EnemyStateBase();
    public EnemyBase EnemyBase;

    public EnemyStateBase preState;
    public EnemyStateBase curState;
    public void Init(CharacterBase character) {
        EnemyBase = EnemyBase;
        curState = idle;
    }

    public void Thinking() {
        curState.Thinking(EnemyBase);
    }

    public void Run() {
        curState.Run(EnemyBase,this);
    }

    public void ChangeState(EnemyStateBase curState,EnemyStateBase targetState) {
        curState.Exit(EnemyBase);
        targetState.Enter(EnemyBase);
        this.preState = curState;
        this.curState = targetState;
    }
}
//TODO:近战和远程俩种怪

//TODO：怪物状态  待机->警戒->逼近->攻击->   受击->远离  远离->远程攻击
public class EnemyStateBase{


    public virtual void Init(EnemyBase enemyBase) {

    }

    public void Thinking(EnemyBase enemyBase) {
        //TODO:思考
    }
    public virtual void Enter(EnemyBase enemyBase) {
        Debug.Log("CharacterState的基础Enter方法");
    }

    public virtual void Run(EnemyBase enemyBase,EnemyStateMeching meching) {
        Debug.Log("CharacterState的基础Run方法");
    }

    public virtual void Exit(EnemyBase enemyBase) {
        Debug.Log("CharacterState的基础Exit方法");
    }
}

public class EnemyIdle : EnemyStateBase {
    public override void Init(EnemyBase enemyBase) {
        //TODO:待机状态，貌似没什么可以做的
    }

    public override void Enter(EnemyBase enemyBase) {
        //TODO：进入状态，停止行动
    }

    public override void Run(EnemyBase enemyBase, EnemyStateMeching meching) {
        //TODO：待机状态，播放待机动画
        //TODO：检测到玩家就进入追击状态
    }

    public override void Exit(EnemyBase enemyBase) {
        
    }
}


