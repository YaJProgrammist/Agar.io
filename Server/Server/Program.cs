using System;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Room.GetInstance().StartGame();
        }
    }
}
