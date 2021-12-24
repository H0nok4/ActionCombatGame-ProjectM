using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRunner : MonoBehaviour
{
    public List<IUpdatable> updatables = new List<IUpdatable>();

    private void Update() {
        //TODO:����ҪС�ı��������п��ܻ���ɾ������Ĳ�����Ӧ��ͳһ����ÿһ֡��ʼɾ����ɾ�����ٱ�����
        for (int i = 0;i < updatables.Count;i++) {
            updatables[i].Update();
        }
    }

    
}


public interface IUpdatable {

    public void Update();
    public void OnInit();
    public void OnDisable();
}