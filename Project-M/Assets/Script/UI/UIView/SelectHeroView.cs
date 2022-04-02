using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUI {
    public class SelectHeroView : UIBase {
        public ButtonBase BackButton;

        public override void OnShow() {
            base.OnShow();
            BackButton.OnMouseClick = () => { UIManager.Instance.CloseCurrentUI(); Debug.Log("OnClick!");};
        }

        public override void OnHide() {
            BackButton.OnMouseClick = null;
        }
    }
}

