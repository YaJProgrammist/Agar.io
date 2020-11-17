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
        private const int SERVER_PORT = 8001;
        private static UDPServer instance = null;
        private static readonly object lockObj = new object();
        private Dictionary<int, Address> playerAddresses;
        private UdpClient server;

        private UDPServer()
        {
            playerAddresses = new Dictionary<int, Address>();
            server = new UdpClient(SERVER_PORT);

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
            try
            {
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
            while(true)
            {
                var result = await server.ReceiveAsync();
                byte[] package = result.Buffer;
                IncomingPackagesManager.HandlePackage(package);
                Console.WriteLine("here");
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
