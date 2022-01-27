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

        var animationController = character.GetComponent<CharacterAnimationEventController>();
        animationController.characterBase = characterBase;

        characterBase.GameObject = character;
        characterBase.Weapon = weapon.GetComponent<Weapon>();
        characterBase.Sprite = character.GetComponentInChildren<SpriteRenderer>();
        characterBase.Animator = character.GetComponent<Animator>();
        characterBase.Rigbody = character.GetComponent<Rigidbody2D>();
        characterBase.StateMeching = new CharacterStateMeching();
        characterBase.StateMeching.Init(characterBase);
        return characterBase;
    }

}
