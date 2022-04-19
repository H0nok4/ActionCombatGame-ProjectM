using System.Collections;
using System.Collections.Generic;
using MyUI;
using UnityEngine;

public class EndView : UIBase {
    public BackButton M_Button;

    public override void OnShow() {
        base.OnShow();
        M_Button.OnMouseClick = BackToMainView;

    }

    public override void OnHide() {
        base.OnHide();
        M_Button.OnMouseClick = null;
    }

    public void BackToMainView() {
        UIManager.Instance.Show(UIManager.Instance.StartPage);
    }
}
