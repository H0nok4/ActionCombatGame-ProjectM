using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGanyu : CharacterBase
{
    public override void NormalAttack(Vector2 inputVec) {
        base.NormalAttack(inputVec);
        //TODO:��Ŀ�귽����һ֧��
    }

    public override void Burst(Vector2 inputVec) {
        base.Burst(inputVec);
        //TODO:�Ե�ǰλ��Ϊ��������һ�����ϵ������ӵ�����
    }

    public override void Skill(Vector2 inputVec) {
        base.Skill(inputVec);
        //TODO���󳷲�ͬʱ����һ�����
    }

    public override void Smash(Vector2 inputVec) {
        Debug.Log("Ganyu���ػ�,��ʼ����");
        //TODO���������ƣ���1�κ�2�Σ�һ�η���һ֧ǿ���ļ������η���ļ����к����AOE�˺�
    }
}
