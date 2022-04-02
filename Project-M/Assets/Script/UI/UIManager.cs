using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager> {
    public List<UIBase> UIPool = new List<UIBase>();

    public UIBase StartPage1;
    public UIBase StartPage2;

    private void Start() {
        Init();
    }
    public void Init() {
        UIPool.Add(StartPage1);
        StartPage1.Show();
    }
    public void Show(UIBase ui) {
        if (!UIPool.Contains(ui)) {
            if (UIPool.Count > 0) {
                UIPool[UIPool.Count - 1].CloseSelf();
            }

            UIPool.Add(ui);
            ui.Show();
        }
        else {
            //TODO:已经开启了，把他移到最上面
            for (int i = 0; i < UIPool.Count; i++) {
                if (UIPool[i] == ui) {
                    UIPool.RemoveAt(i);
                }
                UIPool[UIPool.Count - 1].CloseSelf();
                UIPool.Add(ui);
                ui.Show();
            }
        }

    }

    public void CloseCurrentUI() {
        if (UIPool.Count > 0) {
            UIPool[UIPool.Count - 1].CloseSelf();
            UIPool.RemoveAt(UIPool.Count - 1);
            UIPool[UIPool.Count - 1].Show();
        }
    }
}
