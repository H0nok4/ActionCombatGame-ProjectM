using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DisconnectMsg {
    public int ClientID;
    public int Confirm;
    public string IPAddress;
}
