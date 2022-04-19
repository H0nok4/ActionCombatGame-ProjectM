using UnityEngine;

public class UIBase : MonoBehaviour
{

    public virtual void OnShow() {

    }

    public virtual void OnHide() {

    }

    public virtual void Show() {
        gameObject.SetActive(true);
        OnShow();
    }

    public virtual void CloseSelf() {
        gameObject.SetActive(false);
    }
}
