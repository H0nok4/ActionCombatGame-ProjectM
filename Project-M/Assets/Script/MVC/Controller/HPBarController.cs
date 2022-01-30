using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoSingleton<HPBarController>
{
    public Image HPBarBase;
    public Image HPBar;
    public Image EnergyBarBase;
    public Image EnergyBar;

    public void Init(int maxHealth,int maxEnergy) {
        HPBarBase.rectTransform.sizeDelta = new Vector2(maxHealth,25);
        EnergyBarBase.rectTransform.sizeDelta = new Vector2(maxEnergy * 4,25);
        HPBar.rectTransform.sizeDelta = new Vector2(maxHealth,25);

    }
    public void UpdateHP(int curHealth) {
        HPBar.rectTransform.sizeDelta = new Vector2(curHealth,25);


    }

    public void UpdateEnergy(float curEnergy) {
        EnergyBar.rectTransform.sizeDelta = new Vector2(curEnergy * 4,25);


    }
}
