using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour {
    public CharacterBase characterBase;

    public void StopAttack() {
        characterBase.IsAttack = false;
    }
}
