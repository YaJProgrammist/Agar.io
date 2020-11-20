using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class UDPClient
{
    private readonly Address SERVER_ADDRESS = new Address(8001, "127.0.0.1");
    private readonly int CLIENT_PORT = 8002;
    private static UDPClient instance = null;
    private static readonly object lockObj = new object();
    private UdpClient client;

    static UDPClient()
    {
        GetInstance();
    }

    private UDPClient()
    {
        client = new UdpClient(CLIENT_PORT);

        Thread clientListener = new Thread(ListenToAll);
        clientListener.Start();
    }

    public void SendMessage(byte[] message, int messageLength)
    {
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

    private void ListenToAll()
    {
        while (true)
        {
            var result = client.ReceiveAsync();
            byte[] package = result.Result.Buffer;
            IncomingPackagesManager.HandlePackage(package);
        }
    }

    public static UDPClient GetInstance()
    {
        lock (lockObj)
        {
            if (instance == null)
            {
                instance = new UDPClient();
            }

            return instance;
        }
    }
}
