using System.Collections;
using System.Collections.Generic;
using MyUI;
using UnityEngine;

public class ExitButton : ButtonBase
{
    public override void OnClick() {
        //�ر���Ϸ
        Application.Quit();
    }
}
