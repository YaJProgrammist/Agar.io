using System;
using System.Net.Sockets;

namespace Server
{
    public class UDPServer
    {
        private const int port = 8888;
        private const string server = "127.0.0.1";
        private static UdpClient instance = null;
        private static readonly object lockObj = new object();

        private UDPServer()
        {
        }

        public static UdpClient GetInstance()
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = CreateUdpClient();
                }
                return instance;
            }
        }

        private static UdpClient CreateUdpClient()
        {
            UdpClient client = new UdpClient();

            try
            {
                client.Connect(server, port);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            return client;
        }
    }
}
