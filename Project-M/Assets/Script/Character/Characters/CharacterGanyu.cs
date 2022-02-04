using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGanyu : CharacterBase
{
    public override void NormalAttack(Vector2 inputVec) {
        //TODO:超目标方向发射一支箭
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
        //TODO:以当前位置为中心生成一个不断掉冰棱子的领域
    }

    public override void Skill(Vector2 inputVec) {
        base.Skill(inputVec);
        //TODO：后撤步同时生成一朵冰花
    }

    public override void Smash(Vector2 inputVec,float ChargeTime) {
        Debug.Log("Ganyu的重击,开始蓄力");
        if (ChargeTime < 0.5f) {
            //如果蓄力时间不足，就只是射一支普通的箭
            this.NormalAttack(inputVec);
        }

        //TODO：蓄力机制，分1段和2段，一段发射一支强力的箭，二段发射的箭命中后造成AOE伤害
        if (ChargeTime < 1) {
            //TODO:1段蓄力，发射一支威力十足的箭

        }

        if (ChargeTime >= 1.25) {
            //TODO:2段蓄力，发射一支威力十足的箭，并且在碰撞到物体时还会爆炸造成范围AOE

        }
    }
}
