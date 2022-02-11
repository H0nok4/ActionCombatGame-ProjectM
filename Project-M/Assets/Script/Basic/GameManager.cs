using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {
    public GameObject MainCanvas;

    public Server udpServer = new Server();
    public Client client = new Client();

    public void Start() {
        udpServer.Initialize();
        client.Initialize();

        client.Send();
    }
}
