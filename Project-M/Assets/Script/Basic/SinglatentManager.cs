using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglatentManager : MonoBehaviour
{

    private void Start() {
        GameObjectPool.Init();
        DataCenter.Init();
        UpdateRunner.Init();
    }


}
