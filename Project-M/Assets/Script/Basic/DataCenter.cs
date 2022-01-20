using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter : MonoSingleton<DataCenter>
{
    #region ��Դ��
    private readonly Dictionary<string,GameObject> _projectile = new Dictionary<string,GameObject>();
    private readonly Dictionary<string,GameObject> _character = new Dictionary<string, GameObject>();
    private readonly Dictionary<string,GameObject> _weapon = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, CharacterProperty> _characterProperty = new Dictionary<string, CharacterProperty>();
    private readonly Dictionary<string, GameObject> _UI = new Dictionary<string, GameObject>();
    #endregion

    #region ����
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
        //TODO:δ���Ὣ���ݿ�Ķ�ȡ���������ط�����ֹ������Ϸʱ��̫�ã�������ս����ʼǰ�ĳ�ʼ�����̣��ټӸ�������֮���
        base.OnInitialize();
        LoadRes();
    }


    public void LoadRes() {
        LoadProjectileFromRes();   
        LoadCharacterFromRes();
        LoadWeaponFromRes();
        LoadCharacterPropertyFromRes();
        LoadAllUIFromRes();
    }

    #region ��ȡ��Դ
    public GameObject GetProjectileByName(string name) {
        if (_projectile.ContainsKey(name)) {
            return _projectile[name];
        }

        Debug.LogError($"��ȡProjectileʧ�ܣ�Name = {name}");
        return null;
    }

    public GameObject GetCharacterByName(string name) {
        if (_character.ContainsKey(name)) {
            return _character[name];
        }

        Debug.LogError($"��ȡcharcterʧ�ܣ�Name = {name}");
        return null;
    }

    public GameObject GetWeaponByName(string name) {
        if (_weapon.ContainsKey(name)) {
            return _weapon[name];
        }

        Debug.LogError($"��ȡWeaponʧ�ܣ�Name = {name}");
        return null;
    }

    public CharacterProperty GetCharacterPropertyByName(string name) {
        if (_characterProperty.ContainsKey(name)) {
            return _characterProperty[name];
        }

        Debug.LogError($"��ȡCharacterProperty��Name = {name}");
        return null;
    }

    public GameObject GetUIByName(string name) {
        if (_UI.ContainsKey(name)) {
            return _UI[name];
        }

        Debug.LogError($"��ȡUIʧ�ܣ�Name = {name}");
        return null;
    }
    #endregion


    #region ��ȡ��Դ���ڶ�Ӧ���ֵ���
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
    #endregion

}
