using System.Collections;
using System.Collections.Generic;
using MyUI;
using UnityEngine;

namespace MyUI {
    public class ExitButton : ButtonBase {
        public override void OnClick() {
            //�ر���Ϸ
            Application.Quit();
        }
    }
}

