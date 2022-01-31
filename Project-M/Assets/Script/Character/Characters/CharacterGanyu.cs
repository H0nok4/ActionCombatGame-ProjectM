using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGanyu : CharacterBase
{
    public override void NormalAttack(Vector2 inputVec) {
        base.NormalAttack(inputVec);
        //TODO:超目标方向发射一支箭
    }

    public override void Burst(Vector2 inputVec) {
        base.Burst(inputVec);
        //TODO:以当前位置为中心生成一个不断掉冰棱子的领域
    }

    public override void Skill(Vector2 inputVec) {
        base.Skill(inputVec);
        //TODO：后撤步同时生成一朵冰花
    }

    public override void Smash(Vector2 inputVec) {
        Debug.Log("Ganyu的重击,开始蓄力");
        //TODO：蓄力机制，分1段和2段，一段发射一支强力的箭，二段发射的箭命中后造成AOE伤害
    }
}
