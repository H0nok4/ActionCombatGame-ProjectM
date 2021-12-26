using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]GameObject _character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCharacterFlip(_character.transform.position)) {
            _character.transform.localScale = new Vector3(1,1,1);
        } else {
            _character.transform.localScale = new Vector3(-1,1,1);
        }


    }

    /// <summary>
    /// 需要判断玩家鼠标位置，角色应该面向鼠标那一侧
    /// </summary>
    public bool isCharacterFlip(Vector3 _characterPos) {
        var midPos = new Vector3(Screen.width / 2,Screen.height / 2);
        var mousePos = Input.mousePosition;
        var targetPos = mousePos - midPos;

        if (targetPos.x > _characterPos.x) {
            return true;
        }

        return false;
    }
}
