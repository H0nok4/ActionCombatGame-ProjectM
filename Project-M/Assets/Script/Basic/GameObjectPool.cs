using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    //对象池类，负责游戏的各种对象的生成与销毁
    private static Dictionary<string,List<GameObject>> ObjectPool = new Dictionary<string,List<GameObject>>();
    private GameObject _objectPoolGO;
    public override void OnInitialize() {
        base.OnInitialize();
        _objectPoolGO = new GameObject("@ObjectPool");
    }

    #region 从对象池内创建物体
    public GameObject CreatProjectileFromPool(string projectileName) {
        if (ObjectPool.ContainsKey(projectileName)) {
            //对象池中保存着对应的实例，取出里面的实例并返回
            var objects = ObjectPool[projectileName];
            var needObject = objects[objects.Count - 1];
            objects.RemoveAt(objects.Count - 1);
            //当对象池的对象全都使用的时，需要清除这个名字的链表
            if (objects.Count == 0) {
                ObjectPool.Remove(projectileName);
            }
            needObject.SetActive(true);
            needObject.transform.SetParent(null);

            return needObject;
        } else {
            //对象池中没有保存对应的实例，从数据库中读取
            var projectileGameobejct = DataCenter.Instance.GetProjectileByName(projectileName);
            var instanceGo = Instantiate(projectileGameobejct,Vector3.zero,Quaternion.identity);
            instanceGo.name = projectileName;
            return instanceGo;
        }
    }
    #endregion

    #region 直接创造物体
    public GameObject CreatProjectile(string projectileName) {
        var projectileGameobejct = DataCenter.Instance.GetProjectileByName(projectileName);
        var instanceGo = Instantiate(projectileGameobejct,new Vector3(0,0,0),Quaternion.identity);

        return instanceGo;
    }

    public GameObject CreatCharacter(string characterName) {
        var characterGameObject = DataCenter.Instance.GetCharacterByName(characterName);
        var instanceGo = Instantiate(characterGameObject, Vector3.zero, Quaternion.identity);

        return instanceGo;
    }

    public GameObject CreatWeapon(string weaponName) {
        var weaponGameObject = DataCenter.Instance.GetWeaponByName(weaponName);
        var instanceGo = Instantiate(weaponGameObject, Vector3.zero, Quaternion.identity);

        return instanceGo;
    }

    #endregion

    #region 移除物体到对象池
    public void RemoveGameObjectToPool(GameObject go) {
        if (ObjectPool.ContainsKey(go.name)) {
            ObjectPool[go.name].Add(go);
            go.SetActive(false);
            go.transform.SetParent(_objectPoolGO.transform);
        } else {
            ObjectPool.Add(go.name,new List<GameObject>());
            ObjectPool[go.name].Add(go);
            go.SetActive(false);
            go.transform.SetParent(_objectPoolGO.transform);

        }
    }
    #endregion

}
