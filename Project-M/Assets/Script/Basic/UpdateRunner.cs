using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRunner : MonoBehaviour
{
    public List<IUpdatable> updatables = new List<IUpdatable>();

    private void Update() {
        //TODO:还需要小心遍历过程中可能会有删除对象的操作，应该统一留在每一帧开始删除，删除后再遍历。
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