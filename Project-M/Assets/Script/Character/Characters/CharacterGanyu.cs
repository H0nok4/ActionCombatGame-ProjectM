using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGanyu : CharacterBase
{
    public override void NormalAttack(Vector2 inputVec) {
        //TODO:��Ŀ�귽����һ֧��
        if (IsAttack) {
            return;
        }
        base.NormalAttack(inputVec);
        this.Rigbody.velocity = Vector2.zero;

        var fireObject = GameObjectPool.Instance.CreatProjectileFromPool("Ganyu_Attack_Projectile");
        fireObject.GetComponent<GanyuProjectile>().Init(this);
        var angel = new Vector3(0,0,Vector2.Angle(Vector2.up,inputVec - new Vector2(GameObject.transform.position.x,GameObject.transform.position.y)));
        fireObject.transform.eulerAngles =  angel * -(Sprite.flipX == true ? -1 : 1);
        fireObject.transform.position = Weapon.transform.position;
        var rig = fireObject.GetComponent<Rigidbody2D>();
        rig.velocity = (inputVec - new Vector2(GameObject.transform.position.x,GameObject.transform.position.y)).normalized * 10;



    }

    public override void Burst(Vector2 inputVec) {
        base.Burst(inputVec);
        //TODO:�Ե�ǰλ��Ϊ��������һ�����ϵ������ӵ�����
    }

    public override void Skill(Vector2 inputVec) {
        base.Skill(inputVec);
        //TODO���󳷲�ͬʱ����һ�����
    }

    public override void Smash(Vector2 inputVec,float ChargeTime) {
        Debug.Log("Ganyu���ػ�,��ʼ����");
        if (ChargeTime < 0.5f) {
            //�������ʱ�䲻�㣬��ֻ����һ֧��ͨ�ļ�
            this.NormalAttack(inputVec);
        }

        //TODO���������ƣ���1�κ�2�Σ�һ�η���һ֧ǿ���ļ������η���ļ����к����AOE�˺�
        if (ChargeTime < 1) {
            //TODO:1������������һ֧����ʮ��ļ�

        }

        if (ChargeTime >= 1.25) {
            //TODO:2������������һ֧����ʮ��ļ�����������ײ������ʱ���ᱬը��ɷ�ΧAOE

        }
    }
}
