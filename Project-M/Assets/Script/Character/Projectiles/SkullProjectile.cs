using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullProjectile : ProjectileBase
{
    float startTime;
    public override void Init(EnemyBase enemyBase) {
        base.Init(enemyBase);
        startTime = 0;
    }
    public override void Trigger(Collider2D collision) {
        //TODO:对目标造成少量伤害
        var character = collision.gameObject.GetComponent<CharacterCanDamaged>();
        if (character != null) {
            character.characterBase.OnDamage(EnemySender.Attack);
        }
        GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
    }

    private void Update() {
        startTime += Time.deltaTime;
        if (startTime >= 2) {
            GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
        }
    }
}
