using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

[SerializeField]
public class RoomFightBase : MonoBehaviour{
    public bool IsClear;
    public RoomFightType Type;
    public List<EnemyBase> Enemys;
    public RoomRewardType RewardType;
    public GameObject RewardObject;
    public int RewardNum;

    public void Update() {
        if (IsClear) {
            //TODO:�������������
            return;
        }

        IsClear = Check();
    }
    public virtual bool Check() {

        return false;
    }

    public virtual void StartFight() {

    }

    public virtual void EndFight() {
        //TODO:���Ե��䶫��֮���
        if (RewardType != RoomRewardType.None) {
            
        }
    }
}

public enum RoomFightType {
    Clear,
    OpenBox,
    Destory
}

public enum RoomRewardType {
    None,Box,Coin,HealthBottle
}