using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class UDPClient
{
    public struct UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
    }

    private static readonly Address SERVER_ADDRESS = new Address(8001, "127.0.0.1");
    private readonly int CLIENT_PORT = 8002;
    private static IPEndPoint serverIPEndPoint = new IPEndPoint(IPAddress.Parse(SERVER_ADDRESS.Server), SERVER_ADDRESS.Port);
    private static UDPClient instance = null;
    private static readonly object lockObj = new object();
    public bool messageReceived = false;
    private UdpClient client;

    static UDPClient()
    {
        GetInstance();
    }

    private UDPClient()
    {
        client = new UdpClient();
        client.Connect(serverIPEndPoint);

        Thread clientListener = new Thread(ListenToAll);
        clientListener.Start();
    }

    public void SendMessage(byte[] message, int messageLength)
    {
        try
        {
            Debug.LogWarning("send direction");
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
        IPEndPoint e = new IPEndPoint(IPAddress.Any, CLIENT_PORT);
        UdpClient u = new UdpClient(e);

        while (true)
        {
            messageReceived = false;

            UdpState s = new UdpState();
            s.e = e;
            s.u = u;

            Console.WriteLine("listening for messages");
            u.BeginReceive(new AsyncCallback(ReceiveCallback), s);

            while (!messageReceived)
            {
                Thread.Sleep(50);
            }
        }
    }

    public void ReceiveCallback(IAsyncResult ar)
    {
        UdpClient u = ((UdpState)(ar.AsyncState)).u;
        IPEndPoint e = ((UdpState)(ar.AsyncState)).e;

        byte[] package = u.EndReceive(ar, ref e);
        messageReceived = true;

        IncomingPackagesManager.HandlePackage(package);
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
