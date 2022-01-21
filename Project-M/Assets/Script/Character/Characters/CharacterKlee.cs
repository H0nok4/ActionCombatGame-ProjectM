using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKlee : CharacterBase
{
    public override void NormalAttack(Vector2 inputVec) {
        //TODO:Klee的普通攻击
        
        var fireObject = GameObjectPool.Instance.CreatProjectileFromPool("Klee_Attack_Projectile");
        Vector2 targetVec = (inputVec - new Vector2(GameObject.transform.position.x,GameObject.transform.position.y));//想要攻击的位置
        Vector2 resultPos = new Vector2();
        Vector2 otherPos = new Vector2();

        int count = PMMath.BetweenLineAndCircle(GameObject.transform.position, 3, GameObject.transform.position,
            inputVec, out resultPos, out otherPos);
        Vector3 startPos = new Vector2(Weapon.FirePos.transform.position.x, Weapon.FirePos.transform.position.y);

        Vector3 targetPos = inputVec;
        Vector3 resultVec = resultPos - new Vector2(GameObject.transform.position.x, GameObject.transform.position.y);
        if (count >= 1) {
            var animTime = PMMath.RangeMapping(0.25f, 0.5f, 0f, 3f, resultVec.magnitude);
            new UnityTask(MoveFireObject(fireObject, Weapon.FirePos.transform.position, resultPos, animTime));//攻击动画时间随着攻击位置的长度增加而增加，最低不低于0.25,需要将0~攻击范围映射到0.25~1

        }
        else {
            var animTime = PMMath.RangeMapping(0.25f, 0.5f, 0f, 3f, targetVec.magnitude);
            new UnityTask(MoveFireObject(fireObject, startPos, targetPos, animTime));
        }
        
        Debug.Log("Klee的普通攻击");
    }

    IEnumerator MoveFireObject(GameObject fireObject, Vector2 startPos, Vector2 targetWorldPos, float animationTime = 0.5f) {
        //将动画时间映射到0~1之间来计算动画曲线

        //计算第三个点
        float curT = 0;
        var halfTargetVec = (targetWorldPos - startPos) / 2;
        float angle = 0;
        if (targetWorldPos.x > startPos.x) {
            //如果在右边，计算与右向量的夹角
            angle = Vector2.Angle(halfTargetVec, Vector2.right);
        }
        else {
            //在左半边，就算与左向量的夹角
            angle = Vector2.Angle(halfTargetVec, Vector2.left);
        }
        //夹角最大值为90度，最小值为0度
        var normalVec = new Vector2(0, 1 - Mathf.Abs(angle / 90));

        var thdVec = halfTargetVec + (normalVec + startPos);

        while (curT <= 1) {
            //开始播放
            var curPos = PMMath.CalculateCubicBezierPointfor2C(curT, startPos, thdVec, targetWorldPos);
            fireObject.transform.position = curPos;
            curT = curT + ((Time.fixedDeltaTime) / animationTime);//将动画的时间归一化成0~1的范围
            yield return new WaitForFixedUpdate();
        }

        GameObjectPool.Instance.RemoveGameObjectToPool(fireObject);
        //TODO:对落地区域造成伤害
        //TEMP:需要根据阵容来判断是否造成伤害
        var cols = Physics2D.OverlapCircleAll(new Vector3(fireObject.gameObject.transform.position.x,fireObject.gameObject.transform.position.y,0), 1f);
        foreach (var col in cols) {
            Debug.Log(col.gameObject.name);
            var canDamage = col.gameObject.GetComponent<IDamageable>();
            if (canDamage != null) {
                canDamage.OnDamage(Attack);
            }
        }
        yield break;
    }

    public override void Smash(Vector2 inputVec) {
        Debug.Log("Klee的重击");
        //TODO：对目标方向发射一条爆炸射线
    }
}
