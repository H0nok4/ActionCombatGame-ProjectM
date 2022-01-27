using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEventController : MonoBehaviour {
    public CharacterBase characterBase;

    public void StopAttack(int i) {
        Debug.Log("StopAttack");
        characterBase.IsAttack = false;
        characterBase.Animator.SetBool("IsAttack",false);
    }
}
