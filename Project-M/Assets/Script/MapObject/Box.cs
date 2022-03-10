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
            //TODO:��ҽ��룬����
            animator.SetBool("Outline",true);
            CanOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        Debug.Log("Exit");
        if (col.gameObject.GetComponent<CharacterCanDamaged>() != null) {
            //TODO:��ҽ��룬����
            animator.SetBool("Outline", false);
            CanOpen = false;
        }
    }
    public override int OnDamage(int damagePoint) {
        //���ᱻ����
        return 0;
    }

    public void OpenBox() {
        //TODO:�����ӣ�������Ʒ
        Debug.Log("������");
        GameObjectPool.Instance.RemoveGameObjectToPool(this.gameObject);
    }
}
