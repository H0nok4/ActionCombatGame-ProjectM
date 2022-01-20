using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Barrel : IMapObjectBase
{
    public override int OnDamage(int damagePoint) {

        new UnityTask(PlayAnimation(damagePoint));
        return base.OnDamage(damagePoint);
        
    }

    IEnumerator PlayAnimation(int DamagePoint) {
        var ondamageText = GameObjectPool.Instance.CreatUIFromPool("OnHealthChangeText");
        ondamageText.transform.parent = GameManager.Instance.MainCanvas.transform;
        var gameobjectScreenPos = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up);
        ondamageText.transform.position = Camera.main.ScreenToWorldPoint(gameobjectScreenPos);
        ondamageText.transform.localScale = Vector3.one;
        ondamageText.GetComponent<TMP_Text>().text = $"-{DamagePoint}";

        var animator = GetComponent<Animator>();
        animator.SetBool("OnDamage",true);
        yield return new WaitForSeconds(0.33f);
        animator.SetBool("OnDamage",false);
        GameObjectPool.Instance.RemoveGameObjectToPool(ondamageText);
    }

}
