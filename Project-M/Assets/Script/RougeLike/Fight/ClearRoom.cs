using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRoom : RoomFightBase {
    public List<EnemyBase> targets = new List<EnemyBase>();

    public override bool Check() {
        foreach (var characterBase in targets) {
            if (characterBase.CurHealth > 0) {
                return false;
            }
        }

        return true;
    }

    public override void EndFight() {
        base.EndFight();
        if (RewardType != RoomRewardType.None) {
            for (int i = 0; i < RewardNum; i++) {
                var reward = GameObjectPool.Instance.CreatMapObjectFromPool(RewardObject.name);
                reward.transform.position = transform.position;
            }

        }
    }
}
