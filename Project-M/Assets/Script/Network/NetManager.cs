using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoSingleton<NetManager> {
    public int Port = 8222;
    public short PlayerID = 1;
    private bool isServer = true;
    public Server server;
    public Client client;
    public int frame;
    public Queue<Message> msgQueue = new Queue<Message>();
    private bool start = false;
    private Message msg = null;

    public List<INetObject> netObjects = new List<INetObject>();

    private void Start() {
        if (isServer) {
            server = new Server();
            server.Initialize();
        }
        else {
            client = new Client();
            client.Initialize();
        }

        
        
    }

    private void FixedUpdate() {
        frame++;
        if (frame % 4 == 0) {
            if (isServer) {
                //TODO:从队列中取出消息

            }
            else {
                //TODO:发送消息
                SendCharacterPos();
            }
        }

        if (start == false) {
            if (msgQueue.Count > 1) {
                Debug.Log("Start");
                start = true;
                msg = msgQueue.Dequeue();
            }
        } else {
            while (msgQueue.Count > 0) {
                msg = msgQueue.Dequeue();
            }
        }





    }

    private void Update() {

    }

    public void SendCharacterPos() {
        var characterMsg = new CharacterPosMsg();
        characterMsg.x = CharacterController.Instance.characterPosX;
        characterMsg.y = CharacterController.Instance.characterPosY;

        var msgData = ProtoManager.Serilize(characterMsg);
        var msg = ProtoManager.PackMessage(MessageType.CharacterPos,msgData);
        var msgSerilized = ProtoManager.SerilizeMessage(msg);


        client.Send(msgSerilized);
    }

    public void OnReciveMessage(Message msg) {
        //TODO:处理不同种类的信息
        var type = ((MessageType) msg.Type);
        switch (type) {
            case MessageType.CharacterPos:
                break;
        }
    }
}
