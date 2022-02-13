using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Message {
    public int type;
    public int Length;
    public byte[] Data;
}

public static class ProtoManager{

    /// <summary>
    /// ���л���Ϣ����
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
    /// �����л���Ϣ����
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

    public static Message PackMessage(MessageType type, byte[] data) {
        Message message = new Message();
        message.type = (int)type;
        message.Length = data.Length;
        message.Data = data;
        return message;
    }

    public static byte[] SerilizeMessage(Message message) {
        byte[] bytes = new byte[message.Length + 8];
        Buffer.BlockCopy(BitConverter.GetBytes(message.type),0,bytes,0,4);
        Buffer.BlockCopy(BitConverter.GetBytes(message.Length),0,bytes,4,4);
        Buffer.BlockCopy(message.Data,0,bytes,8,message.Data.Length);
        return bytes;
    }

    public static Message DeserilizeMessage(byte[] data){
        if (data == null || data.Length < 8) {
            return null;
        }

        Message message = new Message();
        int type = BitConverter.ToInt32(data.Take(4).ToArray(),0);
        int length = BitConverter.ToInt32(data.Skip(4).Take(4).ToArray(),0);
        byte[] content = data.Skip(8).Take(length).ToArray();

        message.type = type;
        message.Length = length;
        message.Data = content;

        return message;
    }
}