using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    public bool GetPlayerPressKey(KeyCode code) {
        if (Input.GetKey(code)) {
            return true;
        }

        return false;
    }



    /// <summary>
    /// 需要判断玩家鼠标位置，角色应该面向鼠标那一侧
    /// </summary>
    public bool isCharacterFlip(Vector3 _characterPos) {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > _characterPos.x) {
            return true;
        }

        return false;
    }

}
