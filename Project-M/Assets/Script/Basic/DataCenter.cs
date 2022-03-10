using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataCenter : MonoSingleton<DataCenter>
{
    #region 资源集
    private readonly Dictionary<string,GameObject> _projectile = new Dictionary<string,GameObject>();
    private readonly Dictionary<string,GameObject> _character = new Dictionary<string, GameObject>();
    private readonly Dictionary<string,GameObject> _weapon = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, CharacterProperty> _characterProperty = new Dictionary<string, CharacterProperty>();
    private readonly Dictionary<string, GameObject> _UI = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, GameObject> _MapObject = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, GameObject> _RoomFight = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, GameObject> _Room = new Dictionary<string, GameObject>();
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

    public override void OnInitialize() {
        //TODO:未来会将数据库的读取放在其他地方，防止开启游戏时间太久，可能是战斗开始前的初始化过程，再加个进度条之类的
        base.OnInitialize();
        LoadRes();
    }


    public void LoadRes() {
        LoadProjectileFromRes();   
        LoadCharacterFromRes();
        LoadWeaponFromRes();
        LoadCharacterPropertyFromRes();
        LoadAllUIFromRes();
        LoadAllMapObjectFromRes();
        LoadAllRoomFromRes();
        LoadAllRoomFightFromRes();
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

    public CharacterProperty GetCharacterPropertyByName(string name) {
        if (_characterProperty.ContainsKey(name)) {
            return _characterProperty[name];
        }

        Debug.LogError($"读取CharacterProperty，Name = {name}");
        return null;
    }

    public GameObject GetUIByName(string name) {
        if (_UI.ContainsKey(name)) {
            return _UI[name];
        }

        Debug.LogError($"读取UI失败，Name = {name}");
        return null;
    }

    public GameObject GetMapObjectByName(string name) {
        if (_MapObject.ContainsKey(name)) {
            return _MapObject[name];
        }

        Debug.LogError($"读取MapObject失败，Name = {name}");
        return null;
    }

    public GameObject GetRoomFightByName(string name) {
        if (_RoomFight.ContainsKey(name)) {
            return _RoomFight[name];
        }

        Debug.LogError($"读取RoomFight失败，name = {name}");
        return null;
    }

    public GameObject GetRoomByName(string name) {
        if (_Room.ContainsKey(name)) {
            return _RoomFight[name];
        }

        Debug.LogError($"读取Room失败，name = {name}");
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

    public void LoadCharacterPropertyFromRes() {
        var characterProertys = Resources.LoadAll("Prefabs/CharacterProperty");
        foreach (var property in characterProertys){
            _characterProperty.Add(property.name,(CharacterProperty)property);
        }
    }

    public void LoadAllUIFromRes() {
        var UIs = Resources.LoadAll("Prefabs/UI");
        foreach (var ui in UIs) {
            _UI.Add(ui.name, (GameObject)ui);
        }
    }

    public void LoadAllMapObjectFromRes() {
        var MapObjects = Resources.LoadAll("Prefabs/MapObject");
        foreach (var mapObject in MapObjects) {
            _MapObject.Add(mapObject.name,(GameObject)mapObject);
        }
    }

    public void LoadAllRoomFightFromRes() {
        var RoomFights = Resources.LoadAll("Prefabs/RoomFight");
        foreach (var roomFight in RoomFights) {
            _RoomFight.Add(roomFight.name,(GameObject)roomFight);
        }
        Debug.Log($"加载了{RoomFights.Length}个房间战斗");
    }

    public void LoadAllRoomFromRes() {
        var Rooms = Resources.LoadAll("Prefabs/Map");
        foreach (var room in Rooms) {
            _Room.Add(room.name,(GameObject)room);
        }
        Debug.Log($"加载了{Rooms.Length}个房间");
    }
    #endregion

}
