using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    //对象池类，负责游戏的各种对象的生成与销毁
    private static Dictionary<string,List<GameObject>> ObjectPool = new Dictionary<string,List<GameObject>>();
    private GameObject _objectPoolGO;
    private void Start() {
        _objectPoolGO = Instantiate(new GameObject(),Vector3.zero,Quaternion.identity);
    }

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

    public GameObject CreatProjectile(string projectileName) {
        var projectileGameobejct = DataCenter.Instance.GetProjectileByName(projectileName);
        var instanceGo = Instantiate(projectileGameobejct,new Vector3(0,0,0),Quaternion.identity);

        return instanceGo;
    }

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

}
