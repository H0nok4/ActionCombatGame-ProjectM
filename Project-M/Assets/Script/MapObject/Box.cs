using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Box : MapObjectBase {
    private Animator animator;
    public bool CanOpen = false;
    public override void Init() {
        base.Init();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (CanOpen) {
            if (PlayerController.Instance.GetPlayerPressKey(KeyCode.E)) {
                OpenBox();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Enter");
        if (col.gameObject.GetComponent<CharacterCanDamaged>() != null) {
            //TODO:玩家进入，高亮
            animator.SetBool("Outline",true);
            CanOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        Debug.Log("Exit");
        if (col.gameObject.GetComponent<CharacterCanDamaged>() != null) {
            //TODO:玩家进入，高亮
            animator.SetBool("Outline", false);
            CanOpen = false;
        }
    }
    public override int OnDamage(int damagePoint) {
        //不会被攻击
        return 0;
    }

    public void OpenBox() {
        //TODO:打开箱子，掉落物品
        Debug.Log("打开箱子");
        GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
    }
}
