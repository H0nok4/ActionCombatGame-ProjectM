using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory
{
    public static CharacterBase CreatCharacterInstance(CharacterBase characterBase,string weaponName) {
        //TODO£º×é×°½ÇÉ«
        var character = GameObjectPool.Instance.CreatCharacter(characterBase.CharacterName);
        var weapon = GameObjectPool.Instance.CreatWeapon(weaponName);

        weapon.transform.SetParent(character.transform);
        weapon.transform.position = Vector3.zero;

        characterBase.GameObject = character;
        characterBase.Weapon = weapon.GetComponent<Weapon>();
        characterBase.Sprite = character.GetComponentInChildren<SpriteRenderer>();
        characterBase.Animator = character.GetComponentInChildren<Animator>();
        characterBase.Rigbody = character.GetComponent<Rigidbody2D>();
        return characterBase;
    }

}
