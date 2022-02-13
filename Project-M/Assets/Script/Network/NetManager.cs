using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoSingleton<NetManager> {
    public int Port = 8222;

    public Server server;
    public Client client;

    private void Start() {
        server = new Server();
        client = new Client();
        server.Initialize();
        client.Initialize();

        client.Send();
    }
}
