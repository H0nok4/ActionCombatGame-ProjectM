using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory
{
    public static GameObject CreatCharacterInstance(CharacterBase characterBase) {
        //TODO£º×é×°½ÇÉ«
        var character = GameObjectPool.Instance.CreatCharacter(characterBase.CharacterName);

        return character;
    }

}
