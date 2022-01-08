using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharacterModel _model;
    [SerializeField] GameObject characterGameobject;//��ʱʹ�ã��Ժ���Ҫ����ÿ����ɫ���ɶ�Ӧ��ʵ���ٴ���
    [SerializeField] GameObject characterSpriteobject;
    [SerializeField] GameObject WeaponObject;
    [SerializeField] GameObject tempFireObject;
    [SerializeField] GameObject firePos;
    public PlayerController playerController;
    public CharacterController() {

    }

    public void Awake() {
        //��ʱʹ��
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
        //klee��ͨ����
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            var fireObject = Instantiate(tempFireObject,firePos.transform.position,Quaternion.identity);
            Vector2 targetVec = (playerController.GetPlayerMouseWorldPos() - characterGameobject.transform.position);//��Ҫ������λ��
            Vector2 resultPos = new Vector2();
            Vector2 otherPos = new Vector2();

            int count = Math.BetweenLineAndCircle(characterGameobject.transform.position, 3, characterGameobject.transform.position,
                playerController.GetPlayerMouseWorldPos(), out resultPos, out otherPos);
            Vector3 startPos = new Vector2(firePos.transform.position.x,firePos.transform.position.y);

            Vector3 targetPos = playerController.GetPlayerMouseWorldPos();
            Vector3 resultVec = resultPos - new Vector2(characterGameobject.transform.position.x,characterGameobject.transform.position.y);
            if (count >= 1) {
                var animTime = Math.RangeMapping(0.25f,0.5f,0f,3f,resultVec.magnitude);
                StartCoroutine(MoveFireObject(fireObject, firePos.transform.position, resultPos, animTime));//��������ʱ�����Ź���λ�õĳ������Ӷ����ӣ���Ͳ�����0.25,��Ҫ��0~������Χӳ�䵽0.25~1

            }
            else {
                var animTime = Math.RangeMapping(0.25f,0.5f,0f,3f,targetVec.magnitude);
                StartCoroutine(MoveFireObject(fireObject, startPos,targetPos , animTime));
            }
            
        }
    }

    IEnumerator MoveFireObject(GameObject fireObject,Vector2 startPos,Vector2 targetWorldPos,float animationTime = 0.5f) {
        //������ʱ��ӳ�䵽0~1֮�������㶯������

        //�����������
        float curT = 0;
        var halfTargetVec = (targetWorldPos - startPos) / 2;
        float angle = 0;
        if (targetWorldPos.x > startPos.x) {
            //������ұߣ��������������ļн�
            angle = Vector2.Angle(halfTargetVec,Vector2.right);
        } else {
            //�����ߣ��������������ļн�
            angle = Vector2.Angle(halfTargetVec,Vector2.left);
        }
        //�н����ֵΪ90�ȣ���СֵΪ0��
        var normalVec = new Vector2(0,1 - Mathf.Abs(angle / 90));

        var thdVec = halfTargetVec + (normalVec + startPos); 

        while (curT <= 1) {
            //��ʼ����
            var curPos = Math.CalculateCubicBezierPointfor2C(curT, startPos, thdVec, targetWorldPos);
            fireObject.transform.position = curPos;
            curT = curT + ((Time.fixedDeltaTime) / animationTime);//��������ʱ���һ����0~1�ķ�Χ
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
