using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    //������࣬������Ϸ�ĸ��ֶ��������������
    private static Dictionary<string,List<GameObject>> ObjectPool = new Dictionary<string,List<GameObject>>();
    private GameObject _objectPoolGO;
    public override void OnInitialize() {
        base.OnInitialize();
        _objectPoolGO = new GameObject("@ObjectPool");
    }

    #region �Ӷ�����ڴ�������
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
    #endregion

    #region ֱ�Ӵ�������
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

    #region �Ƴ����嵽�����
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
