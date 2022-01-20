using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : IMapObjectBase
{
    public override int OnDamage(int damagePoint) {

        StartCoroutine(PlayAnimation());
        return base.OnDamage(damagePoint);
        
    }

    IEnumerator PlayAnimation() {
        var animator = GetComponent<Animator>();
        animator.SetBool("OnDamage",true);
        yield return new WaitForSeconds(0.33f);
        animator.SetBool("OnDamage",false);
    }



}
