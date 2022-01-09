using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter : MonoSingleton<DataCenter>
{
    #region ��Դ��
    private Dictionary<string,GameObject> _projectile = new Dictionary<string,GameObject>();

    #endregion

    #region ����
    public Dictionary<string,GameObject> GetProjectile {
        get {
            return _projectile;
        }
    }

    #endregion

    private void Start() {
        //TODO:δ���Ὣ���ݿ�Ķ�ȡ���������ط�����ֹ������Ϸʱ��̫�ã�������ս����ʼǰ�ĳ�ʼ�����̣��ټӸ�������֮���
        
        LoadRes();
    }




    public void LoadRes() {
        LoadProjectileFromRes();   
    }
    #region ��ȡ��Դ
    public GameObject GetProjectileByName(string name) {
        if (_projectile.ContainsKey(name)) {
            return _projectile[name];
        }

        Debug.LogError($"��ȡProjectileʧ�ܣ�Name = {name}");
        return null;
    }
    #endregion


    #region ��ȡ��Դ���ڶ�Ӧ���ֵ���
    public void LoadProjectileFromRes() {
        Debug.Log($"��ʼ��ȡProjectile");
        var projectiles = Resources.LoadAll("Prefabs/Projectiles");
        Debug.Log($"��ȡ���Projectile,Count = {projectiles.Length}");
        foreach (var projectile in projectiles) {
            Debug.Log($"��ȡ��һ���ӵ���Name = {projectile}");
            _projectile.Add(projectile.name,(GameObject)projectile);
        }
    }
    #endregion

}
