using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyUI {
    public class StartButton : ButtonBase {
        public override void OnClick() {
            //TODO:����ѡ��Ӣ��ҳ��
            Debug.Log("����ѡ��Ӣ��ҳ��");
            UIManager.Instance.Show(UIManager.Instance.SelectHeroPage);
        }

    }
}

