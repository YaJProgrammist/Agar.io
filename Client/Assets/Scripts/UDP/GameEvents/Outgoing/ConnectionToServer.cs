using System.Collections.Generic;
using UnityEngine;

public class ConnectionToServer : OutgoingGameEvent
{
    private byte[] myIP;

    public ConnectionToServer()
    {
        myIP = new byte[]{ 127, 0, 0, 1 }; // TODO get real ip
    }

    public override byte[] GetSerialized()
    {
        List<byte> serialized = new List<byte>();

        serialized.Add((byte)OutgoingGameEventTypes.ConnectionToServer);

        serialized.AddRange(myIP);

        return serialized.ToArray();
    }
}
