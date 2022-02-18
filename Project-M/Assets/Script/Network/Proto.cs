using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using UnityEngine;

public class Message {
    public int Type;
    public int Length;
    public byte[] Data;
}

public class StatePackage {
    public int Frame;
    public short PlayerID;
    public int Length;
    public List<Message> Messages = new List<Message>();
}

public static class ProtoManager {

    private const int _frameLength = 4;//�ܰ���֡���ݳ���
    private const int _playerID = 2;//�ܰ�����Һ�
    private const int _totalLengthLength = 4;//�ܰ��������ݵĳ���
    
    private const int _typeLength = 4;//��Ϣ�����ͳ���
    private const int _lengthLength = 4;//��Ϣ�ĳ������ݵĳ���
    
    
    /// <summary>
    /// ���л���Ϣ��Data��������
    /// </summary>
    public static byte[] Serilize(object data) {
        if (data == null || !data.GetType().IsSerializable) {
            Debug.LogError("�����޷����л�������");
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream()) {
            formatter.Serialize(stream,data);
            byte[] result = stream.ToArray();
            return result;
        }
    }

    /// <summary>
    /// �����л���Ϣ��Data��������
    /// </summary>
    public static T Deserilize<T>(byte[] data) where T : class {
        if (data == null || !typeof(T).IsSerializable) {
            Debug.LogError("�����޷����л�������");
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream(data)) {
            object obj = formatter.Deserialize(stream);
            return (T) obj; 
        }
    }

    /// <summary>
    /// ���һ��Message
    /// </summary>
    public static Message PackMessage(MessageType type, byte[] data) {
        Message message = new Message();
        message.Type = (int)type;
        message.Length = data.Length;
        message.Data = data;
        return message;
    }

    public static byte[] SerilizeMessage(Message message) {
        byte[] bytes = new byte[message.Length + 12];
        int index = 0;
        Buffer.BlockCopy(BitConverter.GetBytes(message.Type),0,bytes,index,_typeLength);
        index += _typeLength;
        Buffer.BlockCopy(BitConverter.GetBytes(message.Length),0,bytes,index,_lengthLength);
        index += _lengthLength;

        Buffer.BlockCopy(message.Data,0,bytes,index,message.Data.Length);
        return bytes;
    }

    public static byte[] PackAllMessage(List<Message> messages) {
        //һ�ν����е�messageװ��һ��byte����
        List<byte[]> messageBytes = new List<byte[]>();
        foreach (var message in messages) {
            var bytes = SerilizeMessage(message);
            messageBytes.Add(bytes);
        }

        //�����������ݵ��ܳ��ȣ�������ʼ��byte[]
        int totalLength = 0;
        foreach (var messageByte in messageBytes) {
            totalLength += messageByte.Length;
        }
        Debug.Log(totalLength);
        byte[] allMessage = new byte[totalLength];
        int index = 0;
        //�����е�����װ��ȥ
        foreach (var messageByte in messageBytes) {
            Buffer.BlockCopy(messageByte,0,allMessage,index,messageByte.Length);
            index += messageByte.Length;
        }

        return allMessage;
    }

    
    public static List<Message> UnPackAllMessage(byte[] msg) {
        List<Message> msgList = new List<Message>();
        //��˳��ȡ�����е�Message
        int index = 0;
        while (index < msg.Length) {
            Message message = new Message();
            message.Type = BitConverter.ToInt32(msg.Skip(index).Take(4).ToArray(), 0);
            index += 4;
            message.Length = BitConverter.ToInt32(msg.Skip(index).Take(4).ToArray(), 0);
            index += 4;
            message.Data = msg.Skip(index).Take(message.Length).ToArray();
            index += message.Length;
            msgList.Add(message);
        }

        return msgList;
    }

    public static Message DeserilizeMessage(byte[] data){
        if (data == null || data.Length < 12) {
            return null;
        }

        Message message = new Message();
        int type = BitConverter.ToInt32(data.Take(4).ToArray(),0);
        int length = BitConverter.ToInt32(data.Skip(4).Take(4).ToArray(),0);
        byte[] content = data.Skip(12).Take(length).ToArray();

        message.Type = type;
        message.Length = length;
        message.Data = content;

        return message;
    }

    public static byte[] PackFullPackage(byte[] messages) {
        //һ�ν����е�messageװ���ܰ�������
        byte[] fullPackage = new byte[messages.Length + _frameLength + _playerID + _totalLengthLength];
        int index = 0;
        Buffer.BlockCopy(BitConverter.GetBytes(NetManager.Instance.frame),0,fullPackage,index,_frameLength);
        index += _frameLength;
        Buffer.BlockCopy(BitConverter.GetBytes(NetManager.Instance.PlayerID),0,fullPackage,index,_playerID);
        index += _playerID;
        Buffer.BlockCopy(BitConverter.GetBytes(messages.Length),0,fullPackage,index,_totalLengthLength);
        index += _totalLengthLength;
        Buffer.BlockCopy(messages,0,fullPackage,index,messages.Length);

        return fullPackage;
    }

    public static StatePackage UnPackFullPackage(byte[] fullPack) {
        StatePackage statePackage = new StatePackage();
        statePackage.Frame = BitConverter.ToInt32(fullPack.Take(4).ToArray(), 0);
        statePackage.PlayerID = BitConverter.ToInt16(fullPack.Skip(4).Take(2).ToArray(), 0);
        statePackage.Length = BitConverter.ToInt32(fullPack.Skip(6).Take(4).ToArray(), 0);
        byte[] data = fullPack.Skip(10).Take(statePackage.Length).ToArray();
        statePackage.Messages = UnPackAllMessage(data);

        return statePackage;

    }
}