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
    private EndPoint[] _clientEndPoint;//客户端口

    private int _clientIndex = 0;
    private int _recvSize = 4096;
    private byte[] _bufferRecv;

    public string RecivedMessage = "";

    public void Initialize() {
        _socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
        _endPoint = new IPEndPoint(IPAddress.Any,Port);
        _socket.Bind(_endPoint);
        _bufferRecv = new byte[_recvSize];
        _clientEndPoint = new EndPoint[4];
        _clientEndPoint[_clientIndex++] = new IPEndPoint(IPAddress.Any,1111);
        _socket.BeginReceiveFrom(_bufferRecv,0,_recvSize,SocketFlags.None,ref _clientEndPoint[0],ReceiveClientMessage,_socket);
    }

    public void ReceiveClientMessage(IAsyncResult message) {
        int messageSize = _socket.EndReceiveFrom(message,ref _clientEndPoint[0]);
        if (messageSize > 0) {
            Debug.Log("Recive Msg");
            byte[] _recvedMessage = _bufferRecv.Take(_recvSize).ToArray();

            var msg = ProtoManager.DeserilizeMessage(_recvedMessage);
            try {
                if (msg.Type == (int)MessageType.CharacterPos) {
                    var msgData = ProtoManager.Deserilize<CharacterPosMsg>(msg.Data);
                    NetManager.Instance.testQueue.Enqueue(msgData);
                    Debug.Log($"Recvice characterPosMsg x= {msgData.x},y = {msgData.y}");
                }
            }
            catch{
                Debug.Log("Error");
            }

            _socket.BeginReceiveFrom(_bufferRecv,0,_recvSize,SocketFlags.None,ref _clientEndPoint[0],ReceiveClientMessage,_socket);
        }
    }
}

