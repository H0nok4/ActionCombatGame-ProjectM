using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : Buff
{
    public AttackUp() {
        ID = 1;
        BuffName = "AttackUp";
        durationType = BuffDurationType.always;
        maxDurationTimes = 100;
        curDurationTimes = 0;
        maxOverlayTimes = 5;
        curOverlayTimes = 1;
        tags = new List<string>() {"PowerUp"};
        m_buffEffects = new List<BuffEffect>() {new StateChange_Attack_5(this)};
    }
}
