using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MapObjectBase
{
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Enter");
        if (col.gameObject.GetComponent<CharacterCanDamaged>() != null) {
            //TODO:玩家进入，金币+1并销毁该金币
            BattleManager.Instance.GoldNum++;
            GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
        }
    }
}
