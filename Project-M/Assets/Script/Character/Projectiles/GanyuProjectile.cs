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
        Debug.Log("GanyuProjectile��Trigger����");
        //TODO:��Ŀ����������˺�
        GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
    }

    private void Update() {
        startTime += Time.deltaTime;
        if (startTime >= 2) {
            GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
        }
    }
}
