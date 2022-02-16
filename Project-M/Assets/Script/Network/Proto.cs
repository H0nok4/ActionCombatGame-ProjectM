using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Message {
    public int Type;
    public int Frame;
    public int Length;
    public byte[] Data;
}

public static class ProtoManager {

    private const int _typeLength = 4;
    private const int _lengthLength = 4;
    private const int _frameLength = 4;
    
    /// <summary>
    /// 序列化消息内容
    /// </summary>
    public static byte[] Serilize(object data) {
        if (data == null || !data.GetType().IsSerializable) {
            Debug.LogError("发送无法序列化的数据");
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
    /// 反序列化消息内容
    /// </summary>
    public static T Deserilize<T>(byte[] data) where T : class {
        if (data == null || !typeof(T).IsSerializable) {
            Debug.LogError("接受无法序列化的数据");
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
        Buffer.BlockCopy(BitConverter.GetBytes(message.Frame),0,bytes,index,_frameLength);
        index += _frameLength;
        Buffer.BlockCopy(BitConverter.GetBytes(message.Length),0,bytes,index,_lengthLength);
        index += _lengthLength;

        Buffer.BlockCopy(message.Data,0,bytes,index,message.Data.Length);
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

        message.Type = type;
        message.Length = length;
        message.Data = content;

        return message;
    }
}