using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour,IProjectile {
    public CharacterBase sender;
    public virtual void Init(CharacterBase characterBase) {
        sender = characterBase;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<CharacterCanDamaged>() != null) {
            Debug.Log("On projectile trigger");
            Trigger();
        }

        if (collision.gameObject.GetComponent<MapObjectBase>()!= null) {
            Trigger();
        }
    }

    public virtual void Trigger() {
        Debug.Log("ProjectTile的基类Fire方法");
    }

}
