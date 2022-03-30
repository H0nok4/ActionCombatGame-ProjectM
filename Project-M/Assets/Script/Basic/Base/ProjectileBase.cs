using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour,IProjectile {
    public CharacterBase sender;
    public EnemyBase EnemySender;
    public virtual void Init(CharacterBase characterBase) {
        sender = characterBase;
    }

    public virtual void Init(EnemyBase enemyBase) {
        EnemySender = enemyBase;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<CharacterCanDamaged>() != null) {
            Debug.Log("On projectile trigger");
            Trigger(collision);
        }

        if (collision.gameObject.GetComponent<MapObjectBase>()!= null) {
            Trigger(collision);
        }
    }

    public virtual void Trigger(Collider2D collision) {
        Debug.Log("ProjectTile的基类Fire方法");
    }

}
