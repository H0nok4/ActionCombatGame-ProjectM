using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffDurationType {
    always,
    limit
}

public class Buff {
    public int ID;
    public string BuffName;
    public BuffDurationType durationType;
    public short maxDurationTimes;
    public short curDurationTimes;
    public short maxOverlayTimes;
    public short curOverlayTimes;
    public object[] parameter;
    public List<string> tags;
    protected List<BuffEffect> m_buffEffects;

    public List<BuffEffect> buffEffects {
        get {
            return m_buffEffects;
        }
    }

    public virtual void OnBuffAdd(CharacterBase character) {
        //BUFF可能在添加的时候会改变一些表现，比如护盾，狂暴。
        foreach (var buffEffect in m_buffEffects) {
            buffEffect.OnBuffAdd();
        }
    }

    public virtual void OnBuffRemove(CharacterBase character) {
        //BUFF可能在移除的时候会改变一些表现
        foreach (var buffEffect in m_buffEffects) {
            buffEffect.OnBuffRemove();
        }
    }
}
