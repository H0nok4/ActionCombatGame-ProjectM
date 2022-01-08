using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharacterModel _model;
    [SerializeField] GameObject characterGameobject;//临时使用，以后需要根据每个角色生成对应的实例再传入
    [SerializeField] GameObject characterSpriteobject;
    [SerializeField] GameObject WeaponObject;
    [SerializeField] GameObject tempFireObject;
    [SerializeField] GameObject firePos;
    public PlayerController playerController;
    public CharacterController() {

    }

    public void Awake() {
        //临时使用
        _model = new CharacterModel(characterGameobject,characterSpriteobject);
    }

    private void Update() {
        Fire();
    }

    private void FixedUpdate() {
        MoveCharacter();
        ChangeCharacterDirection();
        UpdateWeaponRotation();

    }

    public void Fire() {
        //普通攻击
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            var fireObject = Instantiate(tempFireObject,firePos.transform.position,Quaternion.identity);
            Vector2 targetVec = (playerController.GetPlayerMouseWorldPos() - characterGameobject.transform.position);//想要攻击的位置
            Vector2 resultPos = new Vector2();
            Vector2 otherPos = new Vector2();

            int count = Math.BetweenLineAndCircle(characterGameobject.transform.position, 3, characterGameobject.transform.position,
                playerController.GetPlayerMouseWorldPos(), out resultPos, out otherPos);
            Debug.Log(count);
            if (count >= 1) {
                StartCoroutine(MoveFireObject(fireObject, firePos.transform.position, resultPos, Mathf.Clamp(resultPos.magnitude / 4, 0.25f, 0.75f)));//攻击动画时间随着攻击位置的长度增加而增加，最低不低于0.25,需要将0~攻击范围映射到0.25~1

            }
            else {
                StartCoroutine(MoveFireObject(fireObject, firePos.transform.position, playerController.GetPlayerMouseWorldPos(), Mathf.Clamp(targetVec.magnitude / 4, 0.25f, 0.75f)));
            }
            
        }
    }

    IEnumerator MoveFireObject(GameObject fireObject,Vector2 startPos,Vector2 targetWorldPos,float animationTime = 0.5f) {
        //将动画时间映射到0~1之间来计算动画曲线
        float curT = 0;
        while (curT <= 1) {
            var thdPos = (targetWorldPos - startPos)/ 2 + (Vector2.up);

            var curPos = Math.CalculateCubicBezierPointfor2C(curT, startPos, thdPos, targetWorldPos);
            fireObject.transform.position = curPos;
            curT = curT + ((Time.fixedDeltaTime) / animationTime);//将动画的时间归一化成0~1的范围
            yield return new WaitForFixedUpdate();
        }

        yield break;
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
        var characterPos = characterGameobject.transform.position;
        var angle = Vector2.Angle(Vector2.up,(new Vector2(mousePos.x,mousePos.y) - new Vector2(characterPos.x,characterPos.y)).normalized);
        WeaponObject.transform.eulerAngles = new Vector3(0,0,angle * -(_model._characterSprite.transform.localScale.x));

        
    }

    public void ChangeCharacterDirection() {
        if (playerController.isCharacterFlip(_model._characterObject.transform.position)) {
            _model._characterSprite.transform.localScale = new Vector3(1,1,1);
        } else {
            _model._characterSprite.transform.localScale = new Vector3(-1,1,1);
        }
    }
}
