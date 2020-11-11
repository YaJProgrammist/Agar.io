using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestUdpClient
{
    class Program
    {
        static string remoteAddress = "127.0.0.1"; // хост для отправки данных
        static int remotePort = 8001; // порт для отправки данных
        static int localPort = 8002; // локальный порт для прослушивания входящих подключений

        static void Main(string[] args)
        {
            UdpClient receiver = new UdpClient(localPort); // UdpClient для получения данных
            IPEndPoint remoteIp = null; // адрес входящего подключения
            //IPEndPoint localpt = new IPEndPoint(IPAddress.Any, 6000);
            //udpServer2.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //udpServer2.Client.Bind(localpt);
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp); // получаем данные
                    string message = Encoding.Unicode.GetString(data);
                    Console.WriteLine("Собеседник: {0}", message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }
    }
}
