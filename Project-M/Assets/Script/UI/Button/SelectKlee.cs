using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace MyUI {
    public class SelectKlee : ButtonBase {
        public override void OnClick() {
            UIManager.Instance.HidePageCanvas();
            BattleManager.Instance.InitBattle("Klee");

        }


    }

}
