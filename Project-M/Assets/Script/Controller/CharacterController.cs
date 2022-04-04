
using System.Collections.Generic;

using UnityEngine;


public class CharacterPosChange {
    public Vector2 Pos;
}

public class CharacterController : MonoSingleton<CharacterController> ,INetObject {
    public Queue<CharacterPosChange> PosChangeMsgQueue = new Queue<CharacterPosChange>();
    public CharacterBase characterBase;
    public float characterPosX = 0;
    public float characterPosY = 0;
    public string Name;
    

    private void Update() {
        if (GameManager.Instance.GameState == GameState.Battle && characterBase != null) {
            characterBase.Update();
            while (PosChangeMsgQueue.Count > 0) {
                var pos = PosChangeMsgQueue.Dequeue();
                characterBase.GameObject.transform.position = pos.Pos;
            }

            if (Input.GetKeyDown(KeyCode.Space)) {

                characterBase.StateMeching.ChangeState(characterBase.StateMeching.curState, BattleManager.dashState);
            }
            else if (PlayerController.Instance.GetSmashKeyDown()) {
                //TODO:����Ϊ�Ҽ���ʼ�����ػ�
                characterBase.StateMeching.ChangeState(characterBase.StateMeching.curState, BattleManager.chargeState);
            }
            else if (PlayerController.Instance.GetPressAttackButton()) {
                characterBase.StartAttack();
            }

            characterPosX = characterBase.GameObject.transform.position.x;
            characterPosY = characterBase.GameObject.transform.position.y;
        }

    }

    private void FixedUpdate() {
        if(characterBase != null) {
            characterBase.FixUpdate();

            //TODO������Щ�Ƶ���ɫ��FixUpdate��
            ChangeCharacterDirection();
            UpdateWeaponRotation();
            characterBase.RecoverEnergy();
        }

    }

    public void SetCharacterPos(float x, float y) {
        characterBase.GameObject.transform.position = new Vector2(x,y);
    }

    public Vector3 GetCharacterPos() {
        return characterBase.GameObject.transform.position;
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

    public Message Send() {
        //TODO:����һ����ɫ��Ϣ������������
        CharacterMsg cMsg = new CharacterMsg();
        cMsg.x = characterBase.GameObject.transform.position.x;
        cMsg.y = characterBase.GameObject.transform.position.y;
        cMsg.CharacterState = characterBase.characterState;

        return ProtoManager.PackMessage(MessageType.Character, ProtoManager.Serilize(cMsg));

    }

    public void Recive(object obj) {
        var characterMessage = ((CharacterMsg) obj);
    }

    public MessageType NeedMessage() {
        return MessageType.Character;
    }
}
