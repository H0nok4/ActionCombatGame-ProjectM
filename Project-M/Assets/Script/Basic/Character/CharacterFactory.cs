using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory
{
    public static GameObject CreatCharacterInstance(CharacterBase characterBase) {
        //TODO����װ��ɫ
        var character = GameObjectPool.Instance.CreatCharacter(characterBase.CharacterName);

        return character;
    }

}
