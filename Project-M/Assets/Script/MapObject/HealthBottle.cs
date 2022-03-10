using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBottle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Enter");
        if (col.gameObject.GetComponent<CharacterCanDamaged>() != null) {
            
            if (CharacterController.Instance.characterBase != null) {
                //TODO:»ØÑª20µã
                var character = CharacterController.Instance.characterBase;
                if (character.CurHealth + 20 > character.MaxHealth) {
                    character.CurHealth = character.MaxHealth;
                }
                else {
                    character.CurHealth += 20;
                }
            }
        }
    }
}
