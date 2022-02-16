using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoSingleton<NetManager> {
    public int Port = 8222;

    public Server server;
    public Client client;
    public int frame;

    private void Start() {
        server = new Server();
        client = new Client();
        server.Initialize();
        client.Initialize();
        
    }

    private void FixedUpdate() {
        frame++;

        if (frame % 4 == 0) {

            var characterMsg = new CharacterPosMsg();
            characterMsg.x = CharacterController.Instance.characterPosX;
            characterMsg.y = CharacterController.Instance.characterPosY;

            var msgData = ProtoManager.Serilize(characterMsg);
            var msg = ProtoManager.PackMessage(MessageType.CharacterPos,msgData);
            var msgSerilized = ProtoManager.SerilizeMessage(msg);


            client.Send(msgSerilized);
        }


    }
}
