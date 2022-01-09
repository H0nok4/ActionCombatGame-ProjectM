using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter : MonoSingleton<DataCenter>
{
    #region 资源集
    private Dictionary<string,GameObject> _projectile = new Dictionary<string,GameObject>();

    #endregion

    #region 属性
    public Dictionary<string,GameObject> GetProjectile {
        get {
            return _projectile;
        }
    }

    #endregion

    private void Start() {
        //TODO:未来会将数据库的读取放在其他地方，防止开启游戏时间太久，可能是战斗开始前的初始化过程，再加个进度条之类的
        
        LoadRes();
    }




    public void LoadRes() {
        LoadProjectileFromRes();   
    }
    #region 调取资源
    public GameObject GetProjectileByName(string name) {
        if (_projectile.ContainsKey(name)) {
            return _projectile[name];
        }

        Debug.LogError($"读取Projectile失败，Name = {name}");
        return null;
    }
    #endregion


    #region 读取资源放在对应的字典内
    public void LoadProjectileFromRes() {
        Debug.Log($"开始读取Projectile");
        var projectiles = Resources.LoadAll("Prefabs/Projectiles");
        Debug.Log($"读取完毕Projectile,Count = {projectiles.Length}");
        foreach (var projectile in projectiles) {
            Debug.Log($"读取到一个子弹，Name = {projectile}");
            _projectile.Add(projectile.name,(GameObject)projectile);
        }
    }
    #endregion

}
