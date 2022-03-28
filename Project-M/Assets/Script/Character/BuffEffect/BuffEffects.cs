using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChange_Attack_5:BuffEffect {
    public StateChange_Attack_5(Buff buff) {
        this.m_Buff = buff;
    }
    public override void StateChange_Add(ref int stateValue, StateType stateType) {
        if (stateType == StateType.Attack) {
            stateValue += 5;
        }
    }
}

public enum StateType {
    Attack,
    MaxHp,
    Defend,
    Speed
}