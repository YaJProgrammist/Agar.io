using System;

namespace Server.Events
{
    public class ConnectionToServer : IncomingGameEvent
    {
        private const int PLAYERS_PORT = 8001;
        private string playerIP;

        public ConnectionToServer(byte[] package)
        {
            if (package[0] != (byte)IncomingGameEventTypes.ConnectionToServer || package.Length != 5)
            {
                Console.WriteLine("incorrect package");
                return;
            }

            playerIP = package[1].ToString() + "." + package[2].ToString() + "." + package[3].ToString() + "." + package[4].ToString();
        }

        public override void Handle()
        {
            Console.WriteLine("Player {0} is here!", playerIP); // TODO delete
            int playerId = IdGenerator.GetInstance().GetId();
            Room.GetInstance().AddPlayer(new Player(playerId));
            UDPServer.GetInstance().AddPlayerClient(playerId, new Address(PLAYERS_PORT, playerIP));
        }
    }
}
