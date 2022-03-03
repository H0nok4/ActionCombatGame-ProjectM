using System.Collections;
using System.Collections.Generic;using System.Linq.Expressions;
using UnityEngine;

public class EnemyStateMeching {
    public EnemyStateBase idle = new EnemyIdle();
    public EnemyStateBase Runway = new EnemyRunaway();
    public EnemyStateBase Approch = new EnemyApproch();

    public EnemyBase EnemyBase;

    public EnemyStateBase preState;
    public EnemyStateBase curState;
    public void Init(EnemyBase enemyBase) {
        EnemyBase = enemyBase;
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

    public virtual void Thinking(EnemyBase enemyBase) {
        //TODO:思考
    }
    public virtual void Enter(EnemyBase enemyBase) {

    }

    public virtual void Run(EnemyBase enemyBase,EnemyStateMeching meching) {

    }

    public virtual void Exit(EnemyBase enemyBase) {

    }
}

public class EnemyIdle : EnemyStateBase {
    public override void Init(EnemyBase enemyBase) {
        //TODO:待机状态，貌似没什么可以做的
    }

    public override void Enter(EnemyBase enemyBase) {
        //TODO：进入状态，停止行动
    }

    public override void Thinking(EnemyBase enemyBase) {
        //TODO：根据玩家的距离判断是不是应该进入攻击状态
        if ((CharacterController.Instance.characterBase.GameObject.transform.position - enemyBase.gameObject.transform.position).magnitude <= 5) {
            Debug.Log("切换状态");
            enemyBase.StateMeching.ChangeState(this,enemyBase.StateMeching.Approch);
        }
        Debug.Log("Thinking!");
    }

    public override void Run(EnemyBase enemyBase, EnemyStateMeching meching) {
        //TODO：待机状态，播放待机动画
        //TODO：检测到玩家就进入追击状态
        
    }

    public override void Exit(EnemyBase enemyBase) {
        
    }
}

public class EnemyApproch:EnemyStateBase {
    public override void Init(EnemyBase enemyBase) {
        base.Init(enemyBase);
    }

    public override void Enter(EnemyBase enemyBase) {
        base.Enter(enemyBase);
        //TODO:进入的时候没啥可以干的
    }

    public override void Run(EnemyBase enemyBase, EnemyStateMeching meching) {
        enemyBase.MoveVec = CharacterController.Instance.characterBase.GameObject.transform.position -
                            enemyBase.transform.position;
    }

    public override void Thinking(EnemyBase enemyBase) {
        if (enemyBase.CurHealth / (float)enemyBase.MaxHealth < 0.2) {   
            //TODO:进入逃跑状态
            enemyBase.StateMeching.ChangeState(this,enemyBase.StateMeching.Runway);
        }
    }

    public override void Exit(EnemyBase enemyBase) {
        base.Exit(enemyBase);
    }
}

public class EnemyRunaway : EnemyStateBase {
    public override void Init(EnemyBase enemyBase) {
        base.Init(enemyBase);
    }

    public override void Enter(EnemyBase enemyBase) {
        base.Enter(enemyBase);
    }

    public override void Thinking(EnemyBase enemyBase) {
        base.Thinking(enemyBase);
    }

    public override void Run(EnemyBase enemyBase, EnemyStateMeching meching) {
        base.Run(enemyBase, meching);
        enemyBase.MoveVec = enemyBase.gameObject.transform.position -
                            CharacterController.Instance.characterBase.GameObject.transform.position;
    }

    public override void Exit(EnemyBase enemyBase) {
        base.Exit(enemyBase);
    }
}


