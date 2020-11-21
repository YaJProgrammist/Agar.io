using Server.Events;
using Server.Events.Incoming;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class UDPServer
    {
        public struct UdpState
        {
            public UdpClient u;
            public IPEndPoint e;
        }

        private const int SERVER_PORT = 8001;
        public const int CLIENT_PORT = 8002;
        private static UDPServer instance = null;
        private static readonly object lockObj = new object();
        private Dictionary<int, Address> playerAddresses;
        public bool messageReceived = false;
        private UdpClient server;

        static UDPServer()
        {
            GetInstance();
        }

        private UDPServer()
        {
            playerAddresses = new Dictionary<int, Address>();
            server = new UdpClient();

            Thread clientListener = new Thread(ListenToAll);
            clientListener.Start();
        }

        public static UDPServer GetInstance()
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = new UDPServer();
                }

                return instance;
            }
        }

        public void AddPlayerClient(int playerId, Address playerAddress)
        {
            try
            {
                if (!playerAddresses.ContainsKey(playerId))
                {
                    Console.WriteLine("Player address added");
                    playerAddresses.Add(playerId, playerAddress);
                    //Thread clientListener = new Thread(ListenToClient);
                    //clientListener.Start(playerId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RemovePlayerClient(int playerId)
        {
            if (playerAddresses.ContainsKey(playerId))
            {
                playerAddresses.Remove(playerId);
            }
        }

        public void SendMessageToAll(byte[] message, int messageLength)
        {
            foreach (var playerId in playerAddresses.Keys)
            {
                SendMessageToPlayer(message, messageLength, playerId);
            }
        }

        public void SendMessageToPlayer(byte[] message, int messageLength, int playerId)
        {
            Console.WriteLine("send {0} {1}", playerId, (OutgoingGameEventTypes)message[0]);
            try
            {
                Console.WriteLine("server {0}, port {1}", playerAddresses[playerId].Server, playerAddresses[playerId].Port);
                server.Connect(playerAddresses[playerId].Server, playerAddresses[playerId].Port);
                server.Send(message, messageLength);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
        }

        private void ListenToAll()
        {
            IPEndPoint e = new IPEndPoint(IPAddress.Any, SERVER_PORT);
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

            for (int i = 0; i < package.Length; i++)
            {
                Console.Write(package[i]);
            }
            Console.WriteLine();

            IncomingPackagesManager.HandlePackage(package);
        }

        private void ListenToClient(object playerIdObj)
        {
            int playerId;

            if (!(playerIdObj is int))
            {
                return;
            }

            playerId = (int)playerIdObj;
            Address playerAddress = playerAddresses[playerId];
            long ipAddress = (long)(uint)IPAddress.NetworkToHostOrder((int)IPAddress.Parse(playerAddress.Server).Address);
            IPEndPoint playerIPEndPoint = new IPEndPoint(ipAddress, playerAddress.Port);

            while (playerAddresses.ContainsKey(playerId))
            {
                try
                {
                    byte[] package = server.Receive(ref playerIPEndPoint);
                    IncomingPackagesManager.HandlePackage(package, playerId);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
