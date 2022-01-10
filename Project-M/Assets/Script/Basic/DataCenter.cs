using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter : MonoSingleton<DataCenter>
{
    #region 资源集
    private Dictionary<string,GameObject> _projectile = new Dictionary<string,GameObject>();
    private Dictionary<string,GameObject> _character = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> _weapon = new Dictionary<string, GameObject>();
    #endregion

    #region 属性
    public Dictionary<string,GameObject> GetProjectile {
        get {
            return _projectile;
        }
    }

    public Dictionary<string, GameObject> GetCharacter {
        get {
            return _character;
        }
    }

    public Dictionary<string, GameObject> GetWeapon {
        get {
            return _weapon;
        }
    }

    #endregion

    private void Start() {
        //TODO:未来会将数据库的读取放在其他地方，防止开启游戏时间太久，可能是战斗开始前的初始化过程，再加个进度条之类的
        
        LoadRes();
    }




    public void LoadRes() {
        LoadProjectileFromRes();   
        LoadCharacterFromRes();
        LoadWeaponFromRes();
    }
    #region 调取资源
    public GameObject GetProjectileByName(string name) {
        if (_projectile.ContainsKey(name)) {
            return _projectile[name];
        }

        Debug.LogError($"读取Projectile失败，Name = {name}");
        return null;
    }

    public GameObject GetCharacterByName(string name) {
        if (_character.ContainsKey(name)) {
            return _character[name];
        }

        Debug.LogError($"读取charcter失败，Name = {name}");
        return null;
    }

    public GameObject GetWeaponByName(string name) {
        if (_weapon.ContainsKey(name)) {
            return _weapon[name];
        }

        Debug.LogError($"读取Weapon失败，Name = {name}");
        return null;
    }
    #endregion


    #region 读取资源放在对应的字典内
    public void LoadProjectileFromRes() {
        var projectiles = Resources.LoadAll("Prefabs/Projectiles");
        foreach (var projectile in projectiles) {
            _projectile.Add(projectile.name,(GameObject)projectile);
        }
    }

    public void LoadCharacterFromRes() {
        var characters = Resources.LoadAll("Prefabs/Characters");
        foreach (var character in characters) {
            _character.Add(character.name,(GameObject)character);
        }
    }

    public void LoadWeaponFromRes() {
        var weapons = Resources.LoadAll("Prefabs/Weapon");
        foreach (var weapon in weapons) {
            _weapon.Add(weapon.name,(GameObject)weapon);
        }

    }
    #endregion

}
