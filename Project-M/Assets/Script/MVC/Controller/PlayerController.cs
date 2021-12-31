using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _character;
    [SerializeField] GameObject _characterSprite;
    Animator animator;
    Rigidbody2D rig;
    int MoveX = 0;
    int MoveY = 0;
    int moveSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        animator = _character.GetComponentInChildren<Animator>();
        rig = _character.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveSpeed = 6;
        } else {
            moveSpeed = 3;
        }
        MoveX = 0;
        MoveY = 0;
        if (isCharacterFlip(_character.transform.position)) {
            _characterSprite.transform.localScale = new Vector3(1,1,1);
        } else {
            _characterSprite.transform.localScale = new Vector3(-1,1,1);
        }

        if (Input.GetKey(KeyCode.A)) {
            MoveX -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            MoveX += 1;
        }
        if (Input.GetKey(KeyCode.W)) {
            MoveY += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            MoveY -= 1;
        }

        if (MoveX != 0 || MoveY != 0) {
            Move(MoveX,MoveY);
        } else {
            animator.SetBool("IsMove",false);
            rig.velocity = new Vector2(0,0);
        }

    }

    public void Move(int x,int y) {
        animator.SetBool("IsMove",true);
        rig.transform.Translate(new Vector3(x,y)* moveSpeed * Time.deltaTime);
        Debug.Log($"MoveX = {x},MoveY = {y}");
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
