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
        private static UDPServer instance = null;
        private static readonly object lockObj = new object();
        private Dictionary<int, Address> playerAddresses;

        private UDPServer()
        {
            playerAddresses = new Dictionary<int, Address>();

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
            if (!playerAddresses.ContainsKey(playerId))
            {
                playerAddresses.Add(playerId, playerAddress);
                Thread clientListener = new Thread(ListenToClient);
                clientListener.Start(playerId);
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
            UdpClient client = new UdpClient();

            try
            {
                client.Connect(playerAddresses[playerId].Server, playerAddresses[playerId].Port);
                client.Send(message, messageLength);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            client.Close();
        }

        private void ListenToAll()
        {
            UdpClient client = new UdpClient();

            while(true)
            {
                byte[] package = client.ReceiveAsync().Result.Buffer;
                IncomingPackagesManager.HandlePackage(package);
            }
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

            UdpClient client = new UdpClient();

            while (playerAddresses.ContainsKey(playerId))
            {
                byte[] package = client.Receive(ref playerIPEndPoint);
                IncomingPackagesManager.HandlePackage(package, playerId);
            }

            client.Close();
        }
    }
}
