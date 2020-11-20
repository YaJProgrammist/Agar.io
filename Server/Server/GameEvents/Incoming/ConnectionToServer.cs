using System;

namespace Server.Events
{
    public class ConnectionToServer : IncomingGameEvent
    {
        private const int PLAYERS_PORT = 8002;
        private string playerIP;

        public ConnectionToServer(byte[] package)
        {
            if (package.Length != 5 || package[0] != (byte)IncomingGameEventTypes.ConnectionToServer)
            {
                Console.WriteLine("Incorrect package");
                return;
            }

            playerIP = package[1].ToString() + "." + package[2].ToString() + "." + package[3].ToString() + "." + package[4].ToString();
        }

        public override void Handle(Room room)
        {
            Console.WriteLine("Player {0} is here!", playerIP); // TODO delete
            int playerId = IdGenerator.GetInstance().GetId();
            room.AddPlayer(new Player(playerId));
            UDPServer.GetInstance().AddPlayerClient(playerId, new Address(PLAYERS_PORT, playerIP));
        }
    }
}
