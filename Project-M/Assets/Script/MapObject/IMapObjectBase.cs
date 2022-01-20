using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class IMapObjectBase : MonoBehaviour,IDamageable {
    public int MaxHealth;
    public int CurHealth;

    public virtual  void Init() {
        MaxHealth = 50;
        CurHealth = MaxHealth;
    }
    private void Start() {
        Init();
    }

    public virtual int OnDamage(int damagePoint) {
        CurHealth -= damagePoint;
        Debug.Log($"Cur Health = {CurHealth}");
        if (CurHealth<= 0) {
            GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
        }

        return CurHealth;
    }
}
