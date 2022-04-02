using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager> {
    public Stack<UIBase> UIPool = new Stack<UIBase>();

    public UIBase StartPage1;
    public UIBase StartPage2;

    private void Start() {
        Init();
    }
    public void Init() {
        UIPool.Push(StartPage1);
        StartPage1.Show();
    }
    public void Show(UIBase ui) {
        if (UIPool.Count > 0) {
            UIPool.Peek().CloseSelf();
        }

        UIPool.Push(ui);
        ui.Show();
    }

    public void CloseCurrentUI() {
        if (UIPool.Count > 0) {
            UIPool.Peek().CloseSelf();
            UIPool.Pop();
            UIPool.Peek().Show();
        }
    }
}
