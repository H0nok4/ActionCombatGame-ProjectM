using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKlee : CharacterBase
{
    public override void NormalAttack(Vector2 inputVec) {
        //TODO:Klee����ͨ����
        
        var fireObject = GameObjectPool.Instance.CreatProjectileFromPool("Klee_Attack_Projectile");
        Vector2 targetVec = (inputVec - new Vector2(GameObject.transform.position.x,GameObject.transform.position.y));//��Ҫ������λ��
        Vector2 resultPos = new Vector2();
        Vector2 otherPos = new Vector2();

        int count = PMMath.BetweenLineAndCircle(GameObject.transform.position, 3, GameObject.transform.position,
            inputVec, out resultPos, out otherPos);
        Vector3 startPos = new Vector2(Weapon.FirePos.transform.position.x, Weapon.FirePos.transform.position.y);

        Vector3 targetPos = inputVec;
        Vector3 resultVec = resultPos - new Vector2(GameObject.transform.position.x, GameObject.transform.position.y);
        if (count >= 1) {
            var animTime = PMMath.RangeMapping(0.25f, 0.5f, 0f, 3f, resultVec.magnitude);
            new UnityTask(MoveFireObject(fireObject, Weapon.FirePos.transform.position, resultPos, animTime));//��������ʱ�����Ź���λ�õĳ������Ӷ����ӣ���Ͳ�����0.25,��Ҫ��0~������Χӳ�䵽0.25~1

        }
        else {
            var animTime = PMMath.RangeMapping(0.25f, 0.5f, 0f, 3f, targetVec.magnitude);
            new UnityTask(MoveFireObject(fireObject, startPos, targetPos, animTime));
        }
        
        Debug.Log("Klee����ͨ����");
    }

    IEnumerator MoveFireObject(GameObject fireObject, Vector2 startPos, Vector2 targetWorldPos, float animationTime = 0.5f) {
        //������ʱ��ӳ�䵽0~1֮�������㶯������

        //�����������
        float curT = 0;
        var halfTargetVec = (targetWorldPos - startPos) / 2;
        float angle = 0;
        if (targetWorldPos.x > startPos.x) {
            //������ұߣ��������������ļн�
            angle = Vector2.Angle(halfTargetVec, Vector2.right);
        }
        else {
            //�����ߣ��������������ļн�
            angle = Vector2.Angle(halfTargetVec, Vector2.left);
        }
        //�н����ֵΪ90�ȣ���СֵΪ0��
        var normalVec = new Vector2(0, 1 - Mathf.Abs(angle / 90));

        var thdVec = halfTargetVec + (normalVec + startPos);

        while (curT <= 1) {
            //��ʼ����
            var curPos = PMMath.CalculateCubicBezierPointfor2C(curT, startPos, thdVec, targetWorldPos);
            fireObject.transform.position = curPos;
            curT = curT + ((Time.fixedDeltaTime) / animationTime);//��������ʱ���һ����0~1�ķ�Χ
            yield return new WaitForFixedUpdate();
        }

        GameObjectPool.Instance.RemoveGameObjectToPool(fireObject);
        //TODO:�������������˺�
        //TEMP:��Ҫ�����������ж��Ƿ�����˺�
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
        Debug.Log("Klee���ػ�");
        //TODO����Ŀ�귽����һ����ը����
    }
}
