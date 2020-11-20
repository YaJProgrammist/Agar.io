using System;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Room room = new Room();
            RoomEventsSender roomEventsSender = new RoomEventsSender(room);
            room.StartGame();
        }
    }
}
