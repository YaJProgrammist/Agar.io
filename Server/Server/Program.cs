using System;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static string remoteAddress = "127.0.0.1"; // хост для отправки данных
        static int remotePort = 8001; // порт для отправки данных
        static int localPort = 8002; // локальный порт для прослушивания входящих подключений

        static void Main(string[] args)
        {
            Room room = new Room();
            room.StartGame();
            /*UdpClient sender = UDPServer.GetInstance();

            try
            {
                while (true)
                {
                    string message = Console.ReadLine(); // сообщение для отправки
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    sender.Send(data, data.Length, remoteAddress, remotePort); // отправка
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }*/
        }
    }
}
