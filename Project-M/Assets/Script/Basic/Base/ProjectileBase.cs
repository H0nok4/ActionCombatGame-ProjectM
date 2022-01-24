using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour,IProjectile {
    public CharacterBase sender;
    public void Init(CharacterBase characterBase) {
        sender = characterBase;
    }

    public virtual void Fire() {
        Debug.Log("ProjectTile的基类Fire方法");
    }

}
