using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoSingleton<NetManager> {
    public int Port = 8222;
    private bool isServer = true;
    public Server server;
    public Client client;
    public int frame;
    public Queue<CharacterPosMsg> testQueue = new Queue<CharacterPosMsg>();
    private bool start = false;
    private CharacterPosMsg msg = null;

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
                if (start == false) {
                    if (testQueue.Count > 1) {
                        Debug.Log("Start");
                        start = true;
                        msg = testQueue.Dequeue();
                    }
                }
                else {
                    if (testQueue.Count > 0) {
                        msg = testQueue.Dequeue();
                    }
                }
            }
            else {
                //TODO:发送消息
                SendCharacterPos();
            }
        }

        //插值移动

    }

    private void Update() {
        if (msg != null) {
            CharacterController.Instance.characterBase.GameObject.transform.position = Vector3.Lerp(
                CharacterController.Instance.characterBase.GameObject.transform.position,new Vector3(msg.x,msg.y,0),
                5 * Time.deltaTime);
        }
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
}
