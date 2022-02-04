using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanyuProjectile : ProjectileBase
{
    float startTime;
    public override void Init(CharacterBase characterBase) {
        base.Init(characterBase);
        startTime = 0;
    }
    public override void Trigger() {
        Debug.Log("GanyuProjectile的Trigger方法");
        //TODO:对目标造成少量伤害
        GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
    }

    private void Update() {
        startTime += Time.deltaTime;
        if (startTime >= 2) {
            GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
        }
    }
}
