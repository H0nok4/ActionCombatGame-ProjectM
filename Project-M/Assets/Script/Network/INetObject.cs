using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INetObject {
    public Message Send();
    public void Recive(object obj);
    public MessageType NeedMessage();

    
}
