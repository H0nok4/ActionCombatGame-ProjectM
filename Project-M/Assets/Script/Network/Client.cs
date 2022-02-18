using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Client {
    public int PlayerID = 1;

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

    public void Send(byte[] sendData) {
        ClientSocket.SendTo(sendData,SeverEndPoint);
    }
}