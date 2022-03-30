using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : EnemyBase
{
    public override void AttackTarget(Vector2 targetPos) {
        //TODO:∑¢…‰“ªÕ≈ª«Ú
        
        var projectile = GameObjectPool.Instance.CreatProjectileFromPool("Skull_Attack_Projectile");
        projectile.GetComponent<ProjectileBase>().Init(this);
        projectile.transform.position = gameObject.transform.position;
        var angel = new Vector3(0, 0, Vector2.Angle(Vector2.up, targetPos - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)));
        var rig = projectile.GetComponent<Rigidbody2D>();
        rig.velocity = (targetPos - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).normalized * 5;
    }
}
