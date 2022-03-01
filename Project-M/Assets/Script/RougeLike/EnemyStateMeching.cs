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
//TODO:��ս��Զ�����ֹ�

//TODO������״̬  ����->����->�ƽ�->����->   �ܻ�->Զ��  Զ��->Զ�̹���
public class EnemyStateBase{


    public virtual void Init(EnemyBase enemyBase) {

    }

    public void Thinking(EnemyBase enemyBase) {
        //TODO:˼��
    }
    public virtual void Enter(EnemyBase enemyBase) {
        Debug.Log("CharacterState�Ļ���Enter����");
    }

    public virtual void Run(EnemyBase enemyBase,EnemyStateMeching meching) {
        Debug.Log("CharacterState�Ļ���Run����");
    }

    public virtual void Exit(EnemyBase enemyBase) {
        Debug.Log("CharacterState�Ļ���Exit����");
    }
}

public class EnemyIdle : EnemyStateBase {
    public override void Init(EnemyBase enemyBase) {
        //TODO:����״̬��ò��ûʲô��������
    }

    public override void Enter(EnemyBase enemyBase) {
        //TODO������״̬��ֹͣ�ж�
    }

    public override void Run(EnemyBase enemyBase, EnemyStateMeching meching) {
        //TODO������״̬�����Ŵ�������
        //TODO����⵽��Ҿͽ���׷��״̬
    }

    public override void Exit(EnemyBase enemyBase) {
        
    }
}


