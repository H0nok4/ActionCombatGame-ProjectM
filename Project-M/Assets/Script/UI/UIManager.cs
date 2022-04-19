using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager> {
    public List<UIBase> UIPool = new List<UIBase>();

    public void Show(UIBase ui) {
        if (!UIPool.Contains(ui)) {
            if (UIPool.Count > 0) {
                UIPool[UIPool.Count - 1].CloseSelf();
            }

            UIPool.Add(ui);
            ui.Show();
        }
        else {
            //已经开启了，把他移到最上面
            for (int i = 0; i < UIPool.Count; i++) {
                if (UIPool[i] == ui) {
                    UIPool.RemoveAt(i);
                }
                UIPool[UIPool.Count - 1].CloseSelf();
                UIPool.Add(ui);
                ui.Show();
                break;
            }
        }
    }

    public void CloseCurrentUI() {
        if (UIPool.Count > 1) {
            UIPool[UIPool.Count - 1].CloseSelf();
            UIPool.RemoveAt(UIPool.Count - 1);
            UIPool[UIPool.Count - 1].Show();
        }
    }

    public GameObject BattleCanvas;
    public GameObject PageCanvas;
    public UIBase StartPage;
    public UIBase SelectHeroPage;
    public UIBase EndPage;

    public void HidePageCanvas() {
        PageCanvas.SetActive(false);
        StartPage.CloseSelf();
        SelectHeroPage.CloseSelf();
        UIPool.Clear();
        BattleCanvas.SetActive(true);
    }

    public void ShowPageCanvas() {
        BattleCanvas.SetActive(false);
        PageCanvas.SetActive(true);
    }
}
