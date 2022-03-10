using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MapObjectBase
{
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Enter");
        if (col.gameObject.GetComponent<CharacterCanDamaged>() != null) {
            //TODO:��ҽ��룬���+1�����ٸý��
            BattleManager.Instance.GoldNum++;
            GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
        }
    }
}
