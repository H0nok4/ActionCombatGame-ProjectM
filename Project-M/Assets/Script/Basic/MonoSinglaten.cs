using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSiglaten<T> : MonoBehaviour where T : MonoBehaviour{
    private static T _instance = null;

    public static T instance {
        get{
            if ((object)MonoSiglaten<T>._instance == null) {
                MonoSiglaten<T>._instance = new T();
            }
            else {
                if (instance != this) {
                    
                }
            }
        }
    }

    private void Awake() {
        if (instance == null) {
            
        }
    }
}
