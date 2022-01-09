using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    //������࣬������Ϸ�ĸ��ֶ��������������
    private static Dictionary<string,List<GameObject>> ObjectPool = new Dictionary<string,List<GameObject>>();
    private GameObject _objectPoolGO;
    private void Start() {
        _objectPoolGO = Instantiate(new GameObject(),Vector3.zero,Quaternion.identity);
    }

    public GameObject CreatProjectileFromPool(string projectileName) {
        if (ObjectPool.ContainsKey(projectileName)) {
            //������б����Ŷ�Ӧ��ʵ����ȡ�������ʵ��������
            var objects = ObjectPool[projectileName];
            var needObject = objects[objects.Count - 1];
            objects.RemoveAt(objects.Count - 1);
            //������صĶ���ȫ��ʹ�õ�ʱ����Ҫ���������ֵ�����
            if (objects.Count == 0) {
                ObjectPool.Remove(projectileName);
            }
            needObject.SetActive(true);
            needObject.transform.SetParent(null);

            return needObject;
        } else {
            //�������û�б����Ӧ��ʵ���������ݿ��ж�ȡ
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
