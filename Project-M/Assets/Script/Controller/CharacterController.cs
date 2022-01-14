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
        characterKlee.Init(KleeProperty);
        characterBase = CharacterFactory.CreatCharacterInstance(characterKlee, "SkyBook");
    }

    private void Update() {
        characterBase.NormalAttack(PlayerController.Instance.GetPlayerMouseWorldPos());
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            characterBase.Dash(PlayerController.Instance.GetPlayerMouseWorldPos());
        }
    }

    private void FixedUpdate() {
        MoveCharacter();
        ChangeCharacterDirection();
        UpdateWeaponRotation();

    }
    public void MoveCharacter() {
        int MoveX = 0;
        int MoveY = 0;
        if (Input.GetKey(KeyCode.LeftShift)) {
            characterBase.MoveSpeed = 6;
        }
        else {
            characterBase.MoveSpeed = 3;
        }

        if (Input.GetKey(KeyCode.A)) {
            MoveX -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            MoveX += 1;
        }
        if (Input.GetKey(KeyCode.W)) {
            MoveY += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            MoveY -= 1;
        }


        characterBase.Move(new Vector2(MoveX,MoveY));
        


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
