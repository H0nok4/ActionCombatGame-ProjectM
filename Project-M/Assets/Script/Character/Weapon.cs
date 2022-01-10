using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public GameObject FirePos;

    public void Rotation(Vector2 mousePos,Vector2 characterPos,int spriteScale) {
        var angle = Vector2.Angle(Vector2.up,(mousePos - characterPos).normalized);
        this.transform.eulerAngles = new Vector3(0,0,angle * -(spriteScale));
    }
}
