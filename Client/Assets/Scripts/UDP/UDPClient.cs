using System;
using System.Net.Sockets;
using UnityEngine;

public static class UDPClient
{
    private static readonly Address SERVER_ADDRESS = new Address(8001, "127.0.0.1");

    public static void SendMessage(byte[] message, int messageLength)
    {
        UdpClient client = new UdpClient();

        try
        {
            client.Connect(SERVER_ADDRESS.Server, SERVER_ADDRESS.Port);
            client.Send(message, messageLength);
        }
        catch (SocketException e)
        {
            Debug.LogError(String.Format("SocketException: {0}", e));
        }
        catch (Exception e)
        {
            Debug.LogError(String.Format("Exception: {0}", e.Message));
        }

        client.Close();
    }
}
