using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyUI {
    public class StartButton : ButtonBase {
        public override void OnClick() {
            //TODO:进入选择英雄页面
            Debug.Log("进入选择英雄页面");
            UIManager.Instance.Show(UIManager.Instance.SelectHeroPage);
        }

    }
}

