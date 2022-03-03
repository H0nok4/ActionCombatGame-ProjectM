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
//TODO:��ս��Զ�����ֹ�

//TODO������״̬  ����->����->�ƽ�->����->   �ܻ�->Զ��  Զ��->Զ�̹���
public class EnemyStateBase{


    public virtual void Init(EnemyBase enemyBase) {

    }

    public virtual void Thinking(EnemyBase enemyBase) {
        //TODO:˼��
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
        //TODO:����״̬��ò��ûʲô��������
    }

    public override void Enter(EnemyBase enemyBase) {
        //TODO������״̬��ֹͣ�ж�
    }

    public override void Thinking(EnemyBase enemyBase) {
        //TODO��������ҵľ����ж��ǲ���Ӧ�ý��빥��״̬
        if ((CharacterController.Instance.characterBase.GameObject.transform.position - enemyBase.gameObject.transform.position).magnitude <= 5) {
            Debug.Log("�л�״̬");
            enemyBase.StateMeching.ChangeState(this,enemyBase.StateMeching.Approch);
        }
        Debug.Log("Thinking!");
    }

    public override void Run(EnemyBase enemyBase, EnemyStateMeching meching) {
        //TODO������״̬�����Ŵ�������
        //TODO����⵽��Ҿͽ���׷��״̬
        
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
        //TODO:�����ʱ��ûɶ���Ըɵ�
    }

    public override void Run(EnemyBase enemyBase, EnemyStateMeching meching) {
        enemyBase.MoveVec = CharacterController.Instance.characterBase.GameObject.transform.position -
                            enemyBase.transform.position;
    }

    public override void Thinking(EnemyBase enemyBase) {
        if (enemyBase.CurHealth / (float)enemyBase.MaxHealth < 0.2) {   
            //TODO:��������״̬
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


