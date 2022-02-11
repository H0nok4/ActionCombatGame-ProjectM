using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;
public class Server
{
    public const int Port = 8222;

    private Socket _socket;//服务器
    private IPEndPoint _endPoint;//服务端口
    private EndPoint _clientEndPoint;//客户端口

    private int _recvSize = 4096;
    private byte[] _bufferRecv;

    public string RecivedMessage = "";

    public void Initialize() {
        _socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
        _endPoint = new IPEndPoint(IPAddress.Any,Port);
        _socket.Bind(_endPoint);
        _bufferRecv = new byte[_recvSize];
        _clientEndPoint = new IPEndPoint(IPAddress.Any,1111);
        _socket.BeginReceiveFrom(_bufferRecv,0,_recvSize,SocketFlags.None,ref _clientEndPoint,ReceiveClientMessage,_socket);
    }

    public void ReceiveClientMessage(IAsyncResult message) {
        int messageSize = _socket.EndReceiveFrom(message,ref _clientEndPoint);
        if (messageSize > 0) {
            byte[] _recvedMessage = _bufferRecv.Take(4).ToArray();
            _socket.BeginReceiveFrom(_bufferRecv,0,_recvSize,SocketFlags.None,ref _clientEndPoint,ReceiveClientMessage,_socket);
            Debug.Log("RecivedMessage = " + BitConverter.ToInt32(_recvedMessage,0).ToString());
        }
    }
}

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
        SeverEndPoint = new IPEndPoint(IPAddress.Parse("59.56.83.11"),8222);
    }

    public void Send() {
        _bufferSend = BitConverter.GetBytes(1);
        ClientSocket.SendTo(_bufferSend,SeverEndPoint);
    }
}
