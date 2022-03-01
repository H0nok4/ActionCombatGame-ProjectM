using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRoom : RoomFightBase {
    public List<CharacterBase> targets = new List<CharacterBase>();

    public override bool Check() {
        foreach (var characterBase in targets) {
            if (characterBase.CurHealth > 0) {
                return false;
            }
        }

        return true;
    }
}
