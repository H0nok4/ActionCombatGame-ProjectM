using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Client {
    public Socket ClientSocket;
    public EndPoint SeverEndPoint;

    public int _recvSize = 4096;
    public byte[] _bufferRecv;
    public byte[] _bufferSend;

    public void Initialize() {
        _bufferRecv = new byte[_recvSize];
        _bufferSend = new byte[_recvSize];
        ClientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
        SeverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),8222);
    }

    public void Send() {
        TestMsg msg = new TestMsg();
        msg.TestInt = 18;
        msg.TestStr = "This is a test Msg";
        msg.TestList = new List<int>();
        msg.TestList.Add(3);
        msg.TestList.Add(545);
        msg.TestList.Add(43);
        msg.TestList.Add(324);
        msg.TestDic = new Dictionary<int, int>();
        msg.TestDic.Add(231,3213);
        msg.TestDic.Add(3213,213124);

        var msgData = ProtoManager.Serilize(msg);
        var message = ProtoManager.PackMessage(MessageType.Test, msgData);
        var sendData = ProtoManager.SerilizeMessage(message);

        ClientSocket.SendTo(sendData,SeverEndPoint);
    }
}