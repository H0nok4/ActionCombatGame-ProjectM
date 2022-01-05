using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharacterModel _model;
    [SerializeField] GameObject characterGameobject;//临时使用，以后需要根据每个角色生成对应的实例再传入
    [SerializeField] GameObject characterSpriteobject;
    [SerializeField] GameObject WeaponObject;
    public PlayerController playerController;
    public CharacterController() {

    }

    public void Awake() {
        //临时使用
        _model = new CharacterModel(characterGameobject,characterSpriteobject);
    }

    private void Update() {
        
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
            _model.moveSpeed = 6;
        } else {
            _model.moveSpeed = 3;
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

        if (MoveX != 0 || MoveY != 0) {
            _model.Move(MoveX,MoveY);
        } else {
            _model.animator.SetBool("IsMove",false);
            _model.CharacterRig.velocity = new Vector2(0,0);
        }

    }

    public void UpdateWeaponRotation() {
        var mousePos = playerController.GetPlayerMouseWorldPos();
        Debug.Log($"mousePos = {mousePos}");
        var characterPos = characterGameobject.transform.position;
        var angle = Vector2.Angle(Vector2.down,(new Vector2(mousePos.x,mousePos.y) - new Vector2(characterPos.x,characterPos.y)).normalized);
        Debug.Log($"angle = {angle}");
        WeaponObject.transform.eulerAngles = new Vector3(0,0,angle);

        
    }

    public void ChangeCharacterDirection() {
        if (playerController.isCharacterFlip(_model._characterObject.transform.position)) {
            _model._characterSprite.transform.localScale = new Vector3(1,1,1);
        } else {
            _model._characterSprite.transform.localScale = new Vector3(-1,1,1);
        }
    }
}
