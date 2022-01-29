using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoSingleton<CharacterController>
{
    public CharacterBase characterBase;
    
    
    public override void OnInitialize() {

    }

    private void Start() {
        //Test
        var KleeProperty = DataCenter.Instance.GetCharacterPropertyByName("Klee");
        var characterKlee = new CharacterKlee();
        characterKlee.Init(KleeProperty,Team.red);
        characterBase = CharacterFactory.CreatCharacterInstance(characterKlee, "SkyBook");
    }

    private void Update() {
        characterBase.Update();

        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            characterBase.StateMeching.ChangeState(characterBase.StateMeching.curState,BattleManager.dashState);
        }else if (PlayerController.Instance.GetNormalSmashInput()) {
            characterBase.Smash(PlayerController.Instance.GetPlayerMouseWorldPos());
        }else if (PlayerController.Instance.GetPressAttackButton()) {
            characterBase.StartAttack();
        }
    }

    private void FixedUpdate() {
        characterBase.FixUpdate();

        //TODO：将这些移到角色的FixUpdate中
        ChangeCharacterDirection();
        UpdateWeaponRotation();
        characterBase.RecoverEnergy();
    }


    public void UpdateWeaponRotation() {
        var mousePos = PlayerController.Instance.GetPlayerMouseWorldPos();
        var characterPos = characterBase.GameObject.transform.position;
        var angle = Vector2.Angle(Vector2.up,(new Vector2(mousePos.x,mousePos.y) - new Vector2(characterPos.x,characterPos.y)).normalized);
        characterBase.Weapon.transform.eulerAngles = new Vector3(0,0,angle * -(characterBase.Sprite.flipX == true?-1:1));


    }

    public void ChangeCharacterDirection() {
        if (PlayerController.Instance.isCharacterFlip(characterBase.GameObject.transform.position)) {
            characterBase.Sprite.flipX = false;
        } else {
            characterBase.Sprite.flipX = true;
        }
    }
}
